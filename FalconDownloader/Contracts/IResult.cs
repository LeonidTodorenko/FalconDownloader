namespace FalconDownloader.Contracts
{
    public interface IResult<T>
    {
        string Error { get; }
        bool HasError { get; }
        T Value { get; }
    }
}
