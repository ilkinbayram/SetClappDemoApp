namespace Core.Resources.OperationResources.Results
{
    public interface IOperationResponse
    {
        bool IsSuccess { get; set; }
        object Result { get; set; }
        string Message { get; set; }
    }
}
