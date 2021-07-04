using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconDownloader.Models
{
    public class IdleSettings
    {
        /// <summary>
        /// //Idle time between check unread emails
        /// </summary>
        public int IdleBetweenTasks { get; set; }
        /// <summary>
        /// //Idle time between connect to server when it is down
        /// </summary>
        public int IdleForStorageServerDown { get; set; }
        /// <summary>
        /// Idle time between connect to server when there is an error
        /// </summary>
        public int IdleAfterError { get; set; }
        /// <summary>
        /// Reconnect times count before send message to admin
        /// </summary>
        public int ReconnectTimesCount { get; set; }
    }
}
