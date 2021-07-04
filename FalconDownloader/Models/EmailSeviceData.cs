using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconDownloader.Models
{
    public class EmailSeviceData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string SubjectSubstring { get; set; }
        public string ProjectsEmail { get; set; }
        public string ForwardMessageAddition { get; set; }
        public string AdminEmail { get; set; }
    }
}
