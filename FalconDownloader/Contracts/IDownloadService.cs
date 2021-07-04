using FalconDownloader.Models;

namespace FalconDownloader.Contracts
{
    public interface IDownloadService
    {
        IResult<DownloadFileStatus> LoadFile(string url, string path, DownloadedFileInfo info);
    }
}
