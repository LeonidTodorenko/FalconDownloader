using FalconDownloader.Contracts;
using FalconDownloader.Models;
using FalconDownloader.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FalconDownloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Path to Excel with Conpany and Stage dictionaries
            var settingsFile = ConfigurationManager.AppSettings["excelPath"];
            //path to downloading files
            var folderpath = ConfigurationManager.AppSettings["storageFolder"];
            //Credentionals for connect to portal and other information for work with storade
            var data = new DownloadSettings
            {
                FileStorageUrl = ConfigurationManager.AppSettings["filesStorageUrl"],
                FilesStorageBaseUrl = ConfigurationManager.AppSettings["filesStorageBaseUrl"],
                Login = ConfigurationManager.AppSettings["loginFileStorage"],
                Password = ConfigurationManager.AppSettings["passFileStorage"],
                LoginElementId = ConfigurationManager.AppSettings["loginElementId"],
                InfinityWaitingString = ConfigurationManager.AppSettings["infinityWaitingString"]
            };
            var emailData = new EmailSeviceData
            {
                // Credentionals for connect to Exchange server
                Login = ConfigurationManager.AppSettings["exchangeLogin"],
                Password = ConfigurationManager.AppSettings["exchangePass"],
                // Find email with this part of subject only
                SubjectSubstring = ConfigurationManager.AppSettings["subjectKeySubstring"],
                ForwardMessageAddition = ConfigurationManager.AppSettings["forwardEmailAddition"],
                ProjectsEmail = ConfigurationManager.AppSettings["projectsEmail"],
                AdminEmail = ConfigurationManager.AppSettings["adminEmail"]
            };
            IExcelService excel = new ExcelService(settingsFile);
            IWordService word = new WordService();
            IEmailService email = new EmailExchangeService(emailData);
            IDownloadService download = new DownloadService(data);

            var idle = new IdleSettings
            {
                IdleBetweenTasks = int.Parse(ConfigurationManager.AppSettings["idleBetweenTasks"]),
                IdleForStorageServerDown = int.Parse(ConfigurationManager.AppSettings["idleForStorageServerDown"]),
                IdleAfterError = int.Parse(ConfigurationManager.AppSettings["reconnectIdleTimeAfterError"]),
                ReconnectTimesCount = int.Parse(ConfigurationManager.AppSettings["reconnectTimesCount"])
            };

            IDownloadManager manager = new DownloadManager(excel, word, email, download, folderpath, idle);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(manager));
        }
    }
}
