using FalconDownloader.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FalconDownloader
{
    /// <summary>
    /// Task Status
    /// </summary>
    public enum TaskStatus
    { 
        [Description("Working")]
        Working,
        [Description("Connecting")]
        ConnectingToExchange,
        [Description("CheckEmails")]
        CheckEmails,
        [Description("DownloadEmails")]
        DownloadEmails,
        [Description("DownloadFiles")]
        DownloadFiles,
        [Description("Ended")]
        Ended
    }

    /// <summary>
    /// Type of Message
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Simple text
        /// </summary>
        [Description("Text")]
        [Color("Black")]
        Text,
        /// <summary>
        /// Information message
        /// </summary>
        [Description("Info")]
        [Color("Blue")]
        Info,
        /// <summary>
        /// Warning message
        /// </summary>
        [Description("Warn")]
        [Color("Goldenrod")]
        Warning,
        /// <summary>
        /// Error message
        /// </summary>
        [Description("Error")]
        [Color("Red")]
        Error
    }

    public enum ViewFileType
    {
        Doc,
        Docx,
        Other
    }

    public enum DownloadFileStatus
    {
        HasFile,
        SessionExpired,
        ServerIsDown,
        NoAttachment,
        CorruptAttachmentLink,
        DonwloadFileError,
        Error
    }
}
