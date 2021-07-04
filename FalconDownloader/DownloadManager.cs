using FalconDownloader.Contracts;
using FalconDownloader.Models;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FalconDownloader
{
    public class DownloadManager : IDownloadManager
    {
        #region Constructor

        public DownloadManager(IExcelService excel, IWordService word, IEmailService email, IDownloadService download, string folderPath, IdleSettings idle)
        {
            _excel = excel;
            _word = word;
            _email = email;
            _download = download;
            _folderPath = folderPath;
            _idle = idle;
        }

        #endregion Constructor

        #region Events

        public event EventHandler ProcessStarted;
        public event EventHandler ProcessStoped;
        public event EventHandler ProcessEnded;
        public event EventHandler<LogMessage> MessageThrowed;
        public event EventHandler<DownloadedFileInfo> FileDownloaded;
        public event EventHandler EmailRead;
        public event EventHandler<TaskInfo> TaskStatusChanged;
        public event EventHandler ServerIsDown;
        public event EventHandler<int> DownloadingError;

        #endregion Events

        #region Public Properties

        public int IdleBetweenTasks
        {
            get { return _idle.IdleBetweenTasks; }
        }
        public int IdleForStorageServerDown 
        { 
            get { return _idle.IdleForStorageServerDown; } 
        }

        public int IdleAfterError
        {
            get { return _idle.IdleAfterError; }
        }
        public int ReconnectTimesCount 
        { 
            get { return _idle.ReconnectTimesCount; } 
        }

        #endregion Public Properties

        #region IDownloadManager Members

        public void Start()
        {
            RaiseMessageEvent(MessageType.Info, "Process is started.");
            if (ProcessStarted != null)
                ProcessStarted(this, new EventArgs());

            _cts = new CancellationTokenSource();
            ProcessEmails();
        }

        public void Stop()
        {
            _cts.Cancel();

            RaiseMessageEvent(MessageType.Warning, "Process was canceled by user.");
            if (ProcessStoped != null)
                ProcessStoped(this, new EventArgs());
        }

        public IResult<bool> Init()
        {
            var res = _excel.LoadDictionaries();
            if(!res.Value) RaiseMessageEvent(MessageType.Error, res.Error);
            else RaiseMessageEvent(MessageType.Text, "Dictionaries is loaded(refreshed).");

            return res;
        }

        #endregion IDownloadManager Members

        #region Private Methods

        private async void ProcessEmails()
        {
            var taskInfo = new TaskInfo { StartedDate = DateTime.Now, Status = TaskStatus.ConnectingToExchange };
            if (TaskStatusChanged != null)
                TaskStatusChanged(this, taskInfo);

            RaiseMessageEvent(MessageType.Text, "Connecting to Exchange Server");
            var emails = await FindEmailsAsync(_cts.Token, taskInfo);
            if(emails != null)
            {
                foreach (var email in emails)
                {
                    if (_cts.IsCancellationRequested)
                        return;

                    RaiseMessageEvent(MessageType.Info, string.Format("Parsing email {0}", email.Subject));

                    var link = ParseLinkFromEmail(email);
                    if (link.HasError)
                    {
                        var cont = await CreateNewAttemptOrForwardToAdmin(email, taskInfo, link.Error);
                        if (cont) continue;
                        else return;
                    }
                    RaiseMessageEvent(MessageType.Text, string.Format("Attachment link: {0}", link.Value));

                    //Now there is the folder path in the body explisit
                    //var path = ParseDirFromEmail(email);

                    var path = ParseExplicitPathToDirFromEMail(email);
                    if(path.HasError)
                    {
                        var cont = await CreateNewAttemptOrForwardToAdmin(email, taskInfo, path.Error);
                        if (cont) continue;
                        else return;
                    }
                    RaiseMessageEvent(MessageType.Text, string.Format("Download path: {0}", path.Value));

                    var info = new DownloadedFileInfo
                    {
                        EmailSubject = email.Subject,
                        DirectoryPath = path.Value
                    };

                    //download the file
                    var res = await DownloadFile(link.Value, path.Value, info);
                    if(res.HasError)
                    {
                        var cont = await CreateNewAttemptOrForwardToAdmin(email, taskInfo, res.Error);
                        if (cont) continue;
                        else return;
                    }

                    if (_cts.IsCancellationRequested)
                        return;

                    if(res.Value == DownloadFileStatus.ServerIsDown)
                    {
                        RaiseMessageEvent(MessageType.Error, "Fileserver is down. Reconnect will be after 24 hours.");
                        RaiseServerIsDownEvent();
                        taskInfo.Status = TaskStatus.Ended;
                        RaiseTaskStatusChangedEvent(taskInfo);
                        return;
                    }

                    if(res.Value == DownloadFileStatus.CorruptAttachmentLink)
                    {
                        var cont = await CreateNewAttemptOrForwardToAdmin(email, taskInfo, "Attachment is corrupted. The message will be forwarded.");
                        if (!cont)
                            return;
                    }

                    if(res.Value == DownloadFileStatus.NoAttachment)
                    {
                        RaiseMessageEvent(MessageType.Warning, "No attachment.");
                    }

                    if (res.Value == DownloadFileStatus.HasFile)
                    {
                        RaiseFileDownloadedEvent(info);
                        RaiseMessageEvent(MessageType.Info, string.Format("File downloaded to {0}.", Path.Combine(info.DirectoryPath, info.FileName)));
                        taskInfo.DownloadedFilesCount++;
                        RaiseTaskStatusChangedEvent(taskInfo);
                    }
                    else
                    {
                        info.EmailWithoutFile = true;
                    }

                    if (_cts.IsCancellationRequested)
                        return;

                    //postprocessing Word files
                    if (info.FileType == ViewFileType.Doc || info.FileType == ViewFileType.Docx)
                    {
                        RaiseMessageEvent(MessageType.Info, "Unlinking Fields from downloaded Word file");

                        var wordPath = Path.Combine(info.DirectoryPath, info.FileName);
                        var wordResult = _word.UnlinkFields(wordPath);

                        if (wordResult.HasError)
                        {
                            var cont = await CreateNewAttemptOrForwardToAdmin(email, taskInfo, wordResult.Error);
                            if (cont) continue;
                            else return;
                        }
                    }

                    if (_cts.IsCancellationRequested)
                        return;

                    await MarkAsRead(email, taskInfo);
                }
            }

            taskInfo.Status = TaskStatus.Ended;
            RaiseTaskStatusChangedEvent(taskInfo);

            if(!_cts.IsCancellationRequested)
            {
                RaiseMessageEvent(MessageType.Info, "Process is ended.");
                if (ProcessEnded != null)
                    ProcessEnded(this, new EventArgs()); 
            }
        }

        /// <summary>
        /// Check fail attempts and forward the email to admin after that
        /// </summary>
        /// <param name="email">Fail email</param>
        /// <param name="taskInfo">Task information</param>
        /// <param name="error">Text of the error</param>
        /// <returns>True - if it should to continue processing, false - it should to break this one</returns>
        private async Task<bool> CreateNewAttemptOrForwardToAdmin(EmailMessage email, TaskInfo taskInfo, string error)
        {
            RaiseMessageEvent(MessageType.Error, error);

            //if it is not the last attempt of this email
            //it needs to stop the process emails and idle for the next attempt
            if(_curruptEmailId != email.Id.UniqueId)
            {
                _curruptEmailId = email.Id.UniqueId;
                _failTimesCount = 1;
            }
            else if(_failTimesCount < _idle.ReconnectTimesCount)
            {
                _failTimesCount++;
            }
            else
            {
                //forward email to admin
                var forward = await ForwardEmailToAdmin(email, error);
                if (forward.HasError)
                {
                    RaiseMessageEvent(MessageType.Error, forward.Error);
                    return true;
                }
                else
                {
                    RaiseMessageEvent(MessageType.Info, string.Format("The message \"{0}\" will be forwarded to admin.", email.Subject));
                }

                //after forwarding mark message as readed
                await MarkAsRead(email, taskInfo);
                return true;
            }

            RaiseDownloadingErrorEvent();
            return false;
        }

        private async System.Threading.Tasks.Task MarkAsRead(EmailMessage email, TaskInfo taskInfo)
        {
            var mark = await System.Threading.Tasks.Task.Factory.StartNew(() => _email.MarkAsRead(email));
            if (mark.HasError)
            {
                RaiseMessageEvent(MessageType.Error, mark.Error);
            }
            else
            {
                RaiseMessageEvent(MessageType.Text, string.Format("Email \"{0}\" was marked as read.", email.Subject));
            }

            taskInfo.UnreadEmailsCount--;
            taskInfo.ReadEmailsCount++;
            RaiseEmailReadEvent();
            RaiseTaskStatusChangedEvent(taskInfo);
        }

        private async Task<IResult<bool>> ForwardEmail(EmailMessage email)
        {
            var res = await System.Threading.Tasks.Task.Factory.StartNew(() => _email.ForwardEmail(email));
            return res;
        }

        private async Task<IResult<bool>> ForwardEmailToAdmin(EmailMessage email, string error)
        {
            var res = await System.Threading.Tasks.Task.Factory.StartNew(() => _email.ForwardEmailToAdmin(email, error));
            return res;
        }

        private async Task<IResult<DownloadFileStatus>> DownloadFile(string link, string path, DownloadedFileInfo info)
        {
            try
            {
                Directory.CreateDirectory(path);
                var res = await System.Threading.Tasks.Task.Factory.StartNew(() => _download.LoadFile(link, path, info));
                return res;
            }
            catch(Exception ex)
            {
                return new Result<DownloadFileStatus>(DownloadFileStatus.Error, ex.Message);
            }
        }

        private void RaiseMessageEvent(MessageType type, string text)
        {
            var msg = new LogMessage {CreationDate = DateTime.Now, Type = type, Message = text};
            if (MessageThrowed != null)
                    MessageThrowed(this, msg);
        }

        private void RaiseTaskStatusChangedEvent(TaskInfo info)
        {
            if (TaskStatusChanged != null)
                TaskStatusChanged(this, info);
        }

        private void RaiseFileDownloadedEvent(DownloadedFileInfo info)
        {
            if (FileDownloaded != null)
                FileDownloaded(this, info);
        }

        private void RaiseEmailReadEvent()
        {
            if (EmailRead != null)
                EmailRead(this, new EventArgs());
        }

        private void RaiseServerIsDownEvent()
        {
            if (ServerIsDown != null)
                ServerIsDown(this, new EventArgs());
        }

        private void RaiseDownloadingErrorEvent()
        {
            if (DownloadingError != null)
                DownloadingError(this, _failTimesCount);
        }

        private async Task<IEnumerable<EmailMessage>> FindEmailsAsync(CancellationToken token, TaskInfo taskInfo)
        {
            var messages = await System.Threading.Tasks.Task.Factory.StartNew(() => _email.FindUnreadEmails());
            if (_cts.IsCancellationRequested)
                return null;
            if (messages.HasError)
            {
                RaiseMessageEvent(MessageType.Error, messages.Error);
                return null;
            }
            if (!messages.Value.Any())
            {
                RaiseMessageEvent(MessageType.Text, "There are no unread emails.");
                return null;
            }
            RaiseMessageEvent(MessageType.Text, string.Format("Found {0} unread emails", messages.Value.Count()));
            taskInfo.Status = TaskStatus.DownloadEmails;
            taskInfo.UnreadEmailsCount = messages.Value.Count();
            RaiseTaskStatusChangedEvent(taskInfo);

            var emails = await System.Threading.Tasks.Task.Factory.StartNew(() => _email.DownloadEmails(messages.Value, token));
            if (_cts.IsCancellationRequested)
                return null;
            if (emails.HasError)
            {
                RaiseMessageEvent(MessageType.Error, emails.Error);
                return null;
            }

            taskInfo.Status = TaskStatus.DownloadEmails;
            RaiseTaskStatusChangedEvent(taskInfo);

            return emails.Value;
        }

        public IResult<string> ParseLinkFromEmail(EmailMessage email)
        {
            try
            {
                var text = email.Body.Text;

                //get link from body like https://is.welocalize.com/*********.doc(x)
                var match = GetFirstMatch(text, @"url=https\S*is.welocalize.com\S*>");
                if (string.IsNullOrWhiteSpace(match))
                {
                    RaiseMessageEvent(MessageType.Text, "There is no file to download.");
                    return new Result<string>(string.Empty);
                }

                var begin = match.IndexOf("https", StringComparison.Ordinal);
                var url = match.Substring(begin, match.Length - begin - 1);
                var link = HttpUtility.UrlDecode(url);

                //return first match
                return new Result<string>(link);
            }
            catch (Exception ex)
            {
                return new Result<string>(string.Empty, ex.Message);
            }
        }

        /// <summary>
        /// Parse a path to the directory from the body
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IResult<string> ParseExplicitPathToDirFromEMail(EmailMessage email)
        {
            try
            {
                //parse the body for the main part of the path
                var key = "Project Folder:";
                var pattern = Regex.Escape(key) + @"\s((.*?)\r\n)+?";
                var matches = Regex.Match(email.Body.Text, pattern, RegexOptions.IgnoreCase);
                if (matches.Length == 0 || string.IsNullOrWhiteSpace(matches.Groups[1].Value))
                {
                    return new Result<string>(null, "There is no Project Folder Path.");
                }
                var path = matches.Groups[1].Value.Trim();

                // parse the body for the stage part of the path
                key = "A file for the task - taskId =";
                pattern = Regex.Escape(key) + @"[^,]*,[^,]*,([^,]*)";
                matches = Regex.Match(email.Body.Text, pattern, RegexOptions.IgnoreCase);
                if (matches.Length == 0 || string.IsNullOrWhiteSpace(matches.Groups[1].Value))
                {
                    return new Result<string>(null, "There is no Stage Path.");
                }
                var stageMatch = matches.Groups[1].Value.Trim();
                if (!_excel.Stages.ContainsKey(stageMatch))
                {
                    var reload = Init();
                    if (reload.HasError || !_excel.Stages.ContainsKey(stageMatch))
                    {
                        return new Result<string>(null, string.Format("There is no code \"{0}\" in stages from Excel.", stageMatch));
                    }
                }
                var stage = _excel.Stages[stageMatch];

                //parse subject for the last part of path
                var direct = GetFirstMatch(email.Subject, @"\s\w*[-\w*]+>\w*-\w*");
                if (string.IsNullOrWhiteSpace(direct))
                {
                    return new Result<string>(null, "Wrong email subject (there is no language direction).");
                }

                direct = direct.Replace(">", "_").Trim();

                //combine the parts
                path = Path.Combine(path, stage, direct);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return new Result<string>(path);
            }
            catch (Exception ex)
            {
                return new Result<string>(string.Empty, ex.Message);
            }
        }

        /// <summary>
        /// Return a path to the directory from the message body by some rules.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IResult<string> ParseDirFromEmail(EmailMessage email)
        {
            //If there is no required company or stage in Excel file
            //it needs reload data from Excel 
            //and if there is still no data it needs to return error

            try
            {
                var subject = email.Subject;

                //get company code from subject
                var compCode = GetFirstMatch(subject, @"\s-\s\w*_\d*_\d*");
                if(string.IsNullOrWhiteSpace(compCode))
                    return new Result<string>(null, "Wrong email subject (there is no company code).");
                compCode = compCode.Substring(3, compCode.Length - 4);
                var code = compCode.Substring(0, compCode.IndexOf("_", StringComparison.Ordinal));
                if(!_excel.Companies.ContainsKey(code))
                {
                    var reload = Init();
                    if(reload.HasError || !_excel.Companies.ContainsKey(code))
                    {
                        return new Result<string>(null, string.Format("There is no code \"{0}\" in conpanies from Excel.", code));
                    }
                }

                var company = _excel.Companies[code];
                //get translate direction
                var direct = GetFirstMatch(subject, @"\s\w*-\w*>\w*-\w*");
                if (string.IsNullOrWhiteSpace(direct))
                {
                    return new Result<string>(null, "Wrong email subject (there is no language direction).");
                }

                direct = direct.Replace(">", "_").Trim();

                //get stage from body (text between company code and languauge direction)
                var stageMatch = GetFirstMatch(email.Body.Text, compCode + @"_\w*,[^,]*");
                if (string.IsNullOrWhiteSpace(stageMatch))
                {
                    return new Result<string>(null, "Wrong email text (there is no stage).");
                }
                var stage = stageMatch.Substring(compCode.Length + 5).Trim();
                if(!_excel.Stages.ContainsKey(stage))
                {
                    var reload = Init();
                    if (reload.HasError || !_excel.Stages.ContainsKey(stage))
                    {
                        return new Result<string>(null, string.Format("There is no code \"{0}\" in stages from Excel.", stage));
                    }
                }
                stage = _excel.Stages[stage];
                var path = string.Format("{0}{1}\\{2}\\{3}\\{4}\\", _folderPath, company, compCode, stage, direct);


                return new Result<string>(path);
            }
            catch (Exception ex)
            {
                return new Result<string>(string.Empty, ex.Message);
            }
        }

        private string GetFirstMatch(string text, string exp)
        {
            var regex = new Regex(exp, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = regex.Matches(text);
            return matches.Count == 0
                ? string.Empty
                : matches[0].Value;
        }

        #endregion Private Methods

        #region Private Members

        private readonly IExcelService _excel;
        private readonly IWordService _word;
        private readonly IEmailService _email;
        private readonly IDownloadService _download;
        private readonly IdleSettings _idle;
        private CancellationTokenSource _cts;
        private readonly string _folderPath;
        private string _curruptEmailId;
        private int _failTimesCount;

        #endregion Private Members
    }
}
