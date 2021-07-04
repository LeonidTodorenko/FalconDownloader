namespace FalconDownloader.Models
{
    public class DownloadedFileInfo
    {
        public string FileName { get; set; }
        public string DirectoryPath { get; set; }
        public string TaskStartTime { get; set; }
        public string EmailSubject { get; set; }
        public ViewFileType FileType { get; set; }
        public bool EmailWithoutFile { get; set; }
    }
}
