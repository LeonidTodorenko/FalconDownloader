using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconDownloader.Models
{
    public class LogMessage
    {
        public MessageType Type { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
