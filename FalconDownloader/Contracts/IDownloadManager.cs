using FalconDownloader.Models;
using System;

namespace FalconDownloader.Contracts
{
    public interface IDownloadManager
    {
        /// <summary>
        /// Occurs whenever a new log message is throwed
        /// </summary>
        event EventHandler<LogMessage> MessageThrowed;
        /// <summary>
        /// Occurs whenever a new file is downloaded
        /// </summary>
        event EventHandler<DownloadedFileInfo> FileDownloaded;
        /// <summary>
        /// Occurs whenever a new email is read
        /// </summary>
        event EventHandler EmailRead;
        /// <summary>
        /// Occurs when a process started
        /// </summary>
        event EventHandler ProcessStarted;
        /// <summary>
        /// Occurs when a process stopped by user
        /// </summary>
        event EventHandler ProcessStoped;
        /// <summary>
        /// Occurs when a process ended
        /// </summary>
        event EventHandler ProcessEnded;
        /// <summary>
        /// Occurs when a current task ended
        /// </summary>
        event EventHandler<TaskInfo> TaskStatusChanged;
        /// <summary>
        /// Occurs when the fileserver is down.
        /// </summary>
        event EventHandler ServerIsDown;
        /// <summary>
        /// Occurs when a file is not downloaded.
        /// </summary>
        event EventHandler<int> DownloadingError;

        /// <summary>
        /// Start process to download emails and files
        /// </summary>
        void Start();
        /// <summary>
        /// Stop process to download emails and files
        /// </summary>
        void Stop();
        /// <summary>
        /// Initialization of manager (load dictionaries)
        /// </summary>
        IResult<bool> Init();

        int IdleBetweenTasks { get; }
        int IdleForStorageServerDown { get; }
        int IdleAfterError { get; }
        int ReconnectTimesCount { get; }
    }
}
