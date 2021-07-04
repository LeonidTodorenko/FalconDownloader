using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconDownloader.Models
{
    public class TaskInfo
    {
        public DateTime StartedDate { get; set; }
        public TaskStatus Status { get; set; }
        public int UnreadEmailsCount { get; set; }
        public int ReadEmailsCount { get; set; }
        public int DownloadedFilesCount { get; set; }
    }
}
