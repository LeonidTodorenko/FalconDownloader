using FalconDownloader.Contracts;
using FalconDownloader.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace FalconDownloader
{
    public partial class MainForm : Form
    {
        #region Constructor

        public MainForm(IDownloadManager manager)
        {
            InitializeComponent();

            _manager = manager;

            if(!DesignMode)
            {
                _manager.MessageThrowed += (o, msg) =>
                {
                    _logMessages.Add(msg);
                    ShowLogMessage(msg);
                };
                _manager.FileDownloaded += (o, info) =>
                {
                    totalDownloadedFiles++;
                    ShowDownloadedFileInfo(info);
                };
                _manager.EmailRead += (o, e) => 
                    totalReadEmails++;
                _manager.ProcessStarted += (o, e) =>
                    ProcessStarted();
                _manager.ProcessStoped += (o, e) =>
                    ManagerStoped();
                _manager.ProcessEnded += (o, e) =>
                    ManagerProcessEnded();
                _manager.TaskStatusChanged += (o, info) =>
                    TaskStatusChanged(info);
                _manager.ServerIsDown += (o, e) =>
                    ServerIsDown();
                _manager.DownloadingError += (o, attempt) =>
                    DownloadingError(attempt);
            }

            _logMessages = new List<LogMessage>();
            _interfaceTimer = new Timer { Interval = 300 };
            _interfaceTimer.Tick += (o, e) => UpdateInteface();
            _betweenProcessTimer = new Timer { Interval = manager.IdleBetweenTasks * 1000 };
            _betweenProcessTimer.Tick += (o, e) =>
                {
                    _betweenProcessTimer.Stop();
                    StartNewProcess();
                };
            _reconnectTimer = new Timer { Interval = _manager.IdleForStorageServerDown * 1000 };
            _reconnectTimer.Tick += (o, e) => ReconnectToServer();
            _reconnectAfterErrorTimer = new Timer { Interval = _manager.IdleAfterError * 1000 };
            _reconnectAfterErrorTimer.Tick += (o, e) => ReconnectAfterDownloadError();

            Text += " v." + Application.ProductVersion;

#if DEBUG
            Text += " --- !!! --- DEBUG --- !!! ---";
#endif
        }

        #endregion Constructor

        #region Events Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                ShowLogMessage(new LogMessage { CreationDate = DateTime.Now, Type = MessageType.Info, Message = "Application is started." });
            }
        }

        private void rbLogFilter_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if(rb != null && rb.Checked)
            {
                //RadioButtons have int values in Tag
                //these values are indexies in MessageType
                var str = string.Format("{0}", rb.Tag);
                _logFilterTypeIndex = string.IsNullOrWhiteSpace(str)
                     ? null
                     : (int?)int.Parse(str);
                FilterLogMessages();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

            var res = _manager.Init();
            btnStart.Enabled = !res.Value;

            if (res.Value)
            {
                _startManagerTime = DateTime.Now;
                _interfaceTimer.Start();
                StartNewProcess();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            _betweenProcessTimer.Stop();

            lblTimeToStartNew.Visible =
                lblTimeToStartNewCaption.Visible = false;

            if(_reconnectTimer.Enabled)
            {
                _startReconnectIdleTime = null;
                _reconnectTimer.Stop();
                lblReconnectTime.Visible =
                    lblServerIsDown.Visible =
                    btnReconnect.Visible =
                    false;
            }

            if(_reconnectAfterErrorTimer.Enabled)
            {
                _startRecinnectIdleAfterErrorTime = null;
                _reconnectAfterErrorTimer.Stop();
                lblReconnectTime.Visible =
                    lblServerIsDown.Visible =
                    btnReconnect.Visible =
                    false;
            }

            _manager.Stop();
        }

        private void lvFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var senderList = (ListView)sender;
            var clickedItem = senderList.HitTest(e.Location).Item;
            if (clickedItem != null && clickedItem.Tag != null)
            {
                var info = clickedItem.Tag as DownloadedFileInfo;
                Process.Start(info.DirectoryPath);
            }
        }

        private void btnReconnect_Click(object sender, EventArgs e)
        {
            ReconnectToServer();
        }

        #endregion Events Handlers

        #region Private Methods

        private void StartNewProcess()
        {
            lblTimeToStartNew.Visible =
                lblTimeToStartNewCaption.Visible = false;
            _manager.Start();
        }

        private void ShowDownloadedFileInfo(DownloadedFileInfo info)
        {
            var toolTip = string.Format("Email: {0}{1}Directory: {2}", info.EmailSubject, Environment.NewLine, info.DirectoryPath);

            var lvi = new ListViewItem(info.FileName);
            lvi.ImageIndex = (int)info.FileType;
            lvi.ToolTipText = toolTip;
            lvi.Tag = info;
            lvFiles.Items.Add(lvi);
        }

        private void ShowLogMessage(LogMessage msg)
        {
            if(!_logFilterTypeIndex.HasValue || _logFilterTypeIndex.Value == (int)msg.Type)
            {
                var line = string.Format("[{0}] {2}", msg.CreationDate.ToString("yyyy-MM-dd HH:mm:ss"), msg.Type.Description(), msg.Message);
                rtbLog.SelectionColor = msg.Type.ForeColor();
                rtbLog.AppendText(rtbLog.TextLength > 0 ? Environment.NewLine + line : line);
            }
        }

        private void FilterLogMessages()
        {
            rtbLog.SuspendLayout();
            rtbLog.Clear();

            var msgs = _logFilterTypeIndex.HasValue
                ? _logMessages.Where(m => m.Type == (MessageType)_logFilterTypeIndex.Value)
                : _logMessages;
            foreach (var msg in msgs)
                ShowLogMessage(msg);

            rtbLog.ResumeLayout(false);
        }

        private void ProcessStarted()
        {
            btnStop.Enabled = true;
            _startTaskTime = DateTime.Now;
            _startIdleTaskTime = null;
            lblStatus.Text = @"Working";
        }

        private void ManagerStoped()
        {
            _startManagerTime = null;
            _startTaskTime = null;
            _startIdleTaskTime = null;
            btnStart.Enabled = true;
            lblStatus.Text = @"Stopped";
            _interfaceTimer.Stop();
            _betweenProcessTimer.Stop();
        }

        private void ManagerProcessEnded()
        {
            //when process is ended it should start the timer for a new process
            _startTaskTime = null;
            _startIdleTaskTime = DateTime.Now;
            _betweenProcessTimer.Start();
            lblTimeToStartNew.Visible =
                lblTimeToStartNewCaption.Visible = true;
        }

        private void UpdateInteface()
        {
            lblReadEmails.Text = totalReadEmails.ToString();
            lblDownloaded.Text = totalDownloadedFiles.ToString();


            if(_startManagerTime.HasValue)
            {
                var interval = DateTime.Now - _startManagerTime.Value;
                lblTime.Text = string.Format("{0:00}:{1:00}:{2:00}", 
                    Math.Floor(interval.TotalHours), 
                    Math.Floor(interval.TotalMinutes % 60),
                    Math.Floor(interval.TotalSeconds % 60)
                );
            }

            if(_startTaskTime.HasValue)
            {
                var interval = DateTime.Now - _startTaskTime.Value;
                lblCurrTaskTime.Text = string.Format("{0:00}:{1:00}:{2:00}", 
                    Math.Floor(interval.TotalHours), 
                    Math.Floor(interval.TotalMinutes % 60),
                    Math.Floor(interval.TotalSeconds % 60)
                );
            }

            if(_startIdleTaskTime.HasValue)
            {
                var interval = _startIdleTaskTime.Value  + new TimeSpan(0, 0, 0, _manager.IdleBetweenTasks) - DateTime.Now;
                lblTimeToStartNew.Text = string.Format("{0:00}:{1:00}:{2:00}", 
                    Math.Floor(interval.TotalHours), 
                    Math.Floor(interval.TotalMinutes % 60),
                    Math.Floor(interval.TotalSeconds % 60)
                );
            }

            if(_startReconnectIdleTime.HasValue)
            {
                var interval = _startReconnectIdleTime.Value + new TimeSpan(0, 0, _manager.IdleForStorageServerDown) - DateTime.Now;
                lblReconnectTime.Text = string.Format("{0:00}:{1:00}:{2:00}", 
                    Math.Floor(interval.TotalHours), 
                    Math.Floor(interval.TotalMinutes % 60), 
                    Math.Floor(interval.TotalSeconds % 60)
                );
            }

            if (_startRecinnectIdleAfterErrorTime.HasValue)
            {
                var interval = _startRecinnectIdleAfterErrorTime.Value + new TimeSpan(0, 0, _manager.IdleAfterError) - DateTime.Now;
                lblReconnectTime.Text = string.Format("{0:00}:{1:00}:{2:00}", 
                    Math.Floor(interval.TotalHours), 
                    Math.Floor(interval.TotalMinutes % 60), 
                    Math.Floor(interval.TotalSeconds % 60)
                );
            }
        }

        private void TaskStatusChanged(TaskInfo info)
        {
            lblCurrTaskStatus.Text = info.Status.Description();
            lblCurrTaskUnreadEmails.Text = (int)info.Status > (int)TaskStatus.ConnectingToExchange ? info.UnreadEmailsCount.ToString() : "- - -";
            lblCurrTaskReadEmails.Text = (int)info.Status > (int)TaskStatus.ConnectingToExchange ? info.ReadEmailsCount.ToString() : "- - -";
            lblCurrTaskDownloadedFiles.Text = (int)info.Status > (int)TaskStatus.ConnectingToExchange ? info.DownloadedFilesCount.ToString() : "- - -";
        }

        private void ServerIsDown()
        {
            lblServerIsDown.Text = ConfigurationManager.AppSettings["serverDownLabelText"];

            lblReconnectTime.Visible =
                lblServerIsDown.Visible =
                btnReconnect.Visible =
                true;

            _startReconnectIdleTime = DateTime.Now;
            _startTaskTime = null;
            _reconnectTimer.Start();
        }

        private void DownloadingError(int attempt)
        {
            lblServerIsDown.Text = string.Format(ConfigurationManager.AppSettings["fileNotDownloadedLabelText"], attempt);

            lblReconnectTime.Visible =
                lblServerIsDown.Visible =
                btnReconnect.Visible =
                true;

            _startRecinnectIdleAfterErrorTime = DateTime.Now;
            _startTaskTime = null;
            _reconnectAfterErrorTimer.Start();
        }

        private void ReconnectToServer()
        {
            _startReconnectIdleTime = null;
            _reconnectTimer.Stop();
            _reconnectAfterErrorTimer.Stop();
            lblReconnectTime.Visible =
                lblServerIsDown.Visible =
                btnReconnect.Visible =
                false;

            StartNewProcess();
        }

        private void ReconnectAfterDownloadError()
        {
            _reconnectAfterErrorTimer.Stop();
            _startRecinnectIdleAfterErrorTime = null;
            lblReconnectTime.Visible =
                lblServerIsDown.Visible =
                btnReconnect.Visible =
                false;

            StartNewProcess();
        }

        #endregion Private Methods

        #region Private Members

        private readonly IDownloadManager _manager;
        private readonly List<LogMessage> _logMessages;

        private DateTime? _startManagerTime;
        private DateTime? _startTaskTime;
        private DateTime? _startIdleTaskTime;
        private DateTime? _startReconnectIdleTime;
        private DateTime? _startRecinnectIdleAfterErrorTime;
        private int? _logFilterTypeIndex;
        private readonly Timer _betweenProcessTimer;
        private readonly Timer _interfaceTimer;
        private readonly Timer _reconnectTimer;
        private readonly Timer _reconnectAfterErrorTimer;

        private int totalReadEmails = 0;
        private int totalDownloadedFiles = 0;

        #endregion Private Members

    }
}
