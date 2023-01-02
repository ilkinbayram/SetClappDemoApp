namespace Core.Utilities.Results
{
    public interface IResponse
    {
        string Message { get; }
        string FullDetail { get; }
    }
}
