namespace FalconDownloader.Contracts
{
    public interface IWordService
    {
        IResult<bool> UnlinkFields(string path);
    }
}
