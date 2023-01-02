namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message) : base(false, false, message)
        {
        }

        public ErrorResult() : base(false, false)
        {
        }
    }
}
