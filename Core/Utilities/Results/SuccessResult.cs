namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message) : base(true, false, message)
        {
        }

        public SuccessResult() : base(true, false)
        {
        }
    }
}
