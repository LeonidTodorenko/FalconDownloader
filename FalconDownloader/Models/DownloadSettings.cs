using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconDownloader.Models
{
    public class DownloadSettings
    {
        public string FileStorageUrl { get; set; }
        public string FilesStorageBaseUrl { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string LoginElementId { get; set; }
        public string InfinityWaitingString { get; set; }
    }
}