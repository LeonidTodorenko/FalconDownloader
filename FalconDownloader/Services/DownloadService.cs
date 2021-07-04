using FalconDownloader.Contracts;
using FalconDownloader.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace FalconDownloader.Services
{
    public class DownloadService : IDownloadService
    {
        public DownloadService(DownloadSettings storageData)
        {
            _storageData = storageData;
        }

        public IResult<DownloadFileStatus> LoadFile(string url, string path, DownloadedFileInfo info)
        {
            //if coockies are too old or null or empty they need be getting
            //session 
            if( string.IsNullOrWhiteSpace(_coockies))
            {
                var init = StorageInit();
                if(init.HasError)
                {
                    return new Result<DownloadFileStatus>(DownloadFileStatus.Error, init.Error);
                }
            }

            //url is nondirect. first we should get intermediate page and get derict url from it
            //intermediate page can contains 4 different strings
            //1) direct link to file
            //2) error 502 (when server is down)
            //3) message "The requested file isn't available. Please wait 15 minutes."
            //4) none of the above
            var directUrl = string.Empty;
            var imKind = GetIntermediatePage(url, ref directUrl);
            {
                //if there is login field it need refresh coockies and try again
                if (imKind.Value == DownloadFileStatus.SessionExpired)
                {
                    var init = StorageInit();
                    if(init.HasError)
                        return new Result<DownloadFileStatus>(DownloadFileStatus.Error, init.Error);

                    imKind = GetIntermediatePage(url, ref directUrl);
                }

                if (imKind.HasError)
                    return new Result<DownloadFileStatus>(DownloadFileStatus.Error, imKind.Error);

                //return all statuses except HasFile (there is an error)
                if (imKind.Value != DownloadFileStatus.HasFile)
                {
                    return imKind;
                }
            }

            //get filename
            var parameter = HttpUtility.ParseQueryString(directUrl).Get("_falconDownload_WAR_onDemandPlatform_path");
            var fileName = parameter.Substring(parameter.LastIndexOf("/", StringComparison.CurrentCultureIgnoreCase) + 1);
            var fullPath = GetUniqueNameForDuplicated(path + "\\" + fileName);

            info.FileName = Path.GetFileName(fullPath);
            var ext = Path.GetExtension(fullPath).ToLower();
            info.FileType = ext.Equals(".docx") ? ViewFileType.Docx : ext.Equals(".doc") ? ViewFileType.Doc : ViewFileType.Other;
            var downloaded = DownloadFile(directUrl, fullPath);
            return downloaded.Value
                ? new Result<DownloadFileStatus>(DownloadFileStatus.HasFile)
                : new Result<DownloadFileStatus>(DownloadFileStatus.DonwloadFileError, downloaded.Error);
        }

        private IResult<bool> StorageInit()
        {
            Uri url = new Uri(_storageData.FilesStorageBaseUrl);
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            CookieContainer cookieJar = new CookieContainer();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookieJar;
            request.Method = "GET";
            HttpStatusCode responseStatus;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                responseStatus = response.StatusCode;
                url = request.Address;
            }

            if (responseStatus == HttpStatusCode.OK)
            {
                request = (HttpWebRequest)WebRequest.Create(_storageData.FileStorageUrl);
                request.Referer = url.ToString();
                request.CookieContainer = cookieJar;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                using (Stream requestStream = request.GetRequestStream())
                    using (StreamWriter requestWriter = new StreamWriter(requestStream, Encoding.ASCII))
                    {
                        string postData = string.Format("_58_redirect=&_58_login={0}&_58_password={1}", _storageData.Login, Uri.EscapeDataString(_storageData.Password));
                        requestWriter.Write(postData);
                    }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream responseStream = response.GetResponseStream())
                        using (StreamReader responseReader = new StreamReader(responseStream))
                        {
                            string responseContent = responseReader.ReadToEnd();
                            if (responseContent.Contains(_storageData.LoginElementId))
                            {
                                return new Result<bool>(false, "Cannot connect to server.");
                            }
                            else
                            {
                                _coockies = request.Headers.GetValues(2).FirstOrDefault();
                                return new Result<bool>(true);
                            }
                        }
            }
            else
            {
                return new Result<bool>(false, "Cannot connect to server.");
            } 
        }

        private IResult<DownloadFileStatus> GetIntermediatePage(string link, ref string directLink)
        {
            Uri url = new Uri(link);
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            CookieContainer cookieJar = new CookieContainer();
            cookieJar.Add(GetCoockiesFromString(_coockies));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookieJar;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                    using (StreamReader responseReader = new StreamReader(responseStream))
                    {
                        //if status 502 or 500 - return and switch application to wait mode
                        if(response.StatusCode == HttpStatusCode.BadGateway || response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            return new Result<DownloadFileStatus>(DownloadFileStatus.ServerIsDown);
                        }

                        if(response.StatusCode != HttpStatusCode.OK)
                        {
                            return new Result<DownloadFileStatus>(DownloadFileStatus.Error, "Cannot connect to server.");
                        }

                        string responseContent = responseReader.ReadToEnd();

                        //check contains direct link
                        var regex = new Regex(@"href\S*>Download</a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        var matches = regex.Matches(responseContent);
                        if (matches.Count != 0)
                        {
                            //cut url from matched string
                            directLink = matches[0].Value.Substring(6, matches[0].Value.Length - 20);
                            return new Result<DownloadFileStatus>(DownloadFileStatus.HasFile);
                        }

                        //check contains field for input login (session expired)
                        regex = new Regex(_storageData.LoginElementId, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        matches = regex.Matches(responseContent);
                        if (matches.Count != 0)
                            return new Result<DownloadFileStatus>(DownloadFileStatus.SessionExpired);

                        //check contains sentense to wait 15 minutes
                        regex = new Regex(_storageData.InfinityWaitingString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        matches = regex.Matches(responseContent);
                        if (matches.Count != 0)
                            return new Result<DownloadFileStatus>(DownloadFileStatus.CorruptAttachmentLink);

                        //none of the above
                        return new Result<DownloadFileStatus>(DownloadFileStatus.NoAttachment, "No link in page");
                    } 
        }

        private CookieCollection GetCoockiesFromString(string str)
        {
            var coockies = new CookieCollection();
            var items = str.Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);

            foreach(var item in items)
            {
                var index = item.IndexOf('=');
                var coockie = new Cookie(item.Substring(0, index), item.Substring(index + 1))
                {
                    Domain = _storageData.FilesStorageBaseUrl.Replace("/", "").Replace("https:", "")
                };
                coockies.Add(coockie);
            }

            return coockies;
        }

        private string GetUniqueNameForDuplicated(string strFullPath)
        {
            int count = 1;

            string fileNameOnly = Path.GetFileNameWithoutExtension(strFullPath);
            string extension = Path.GetExtension(strFullPath);
            string path = Path.GetDirectoryName(strFullPath);
            string newFullPath = strFullPath;

            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
            }

            return newFullPath;
        }

        private IResult<bool> DownloadFile(string url, string fullPath)
        {
            try
            {
                var myClient = new WebClient();
                myClient.Headers.Add("Cookie: " + _coockies);
                myClient.DownloadFile(url, fullPath);
                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                while(ex != null)
                {
                    sb.Append(ex.Message + " ");
                    ex = ex.InnerException;
                }

                return new Result<bool>(false, sb.ToString());
            }
        }

        private readonly DownloadSettings _storageData;
        private string _coockies;
    }
}
