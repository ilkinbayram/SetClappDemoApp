using System.Collections.Generic;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, false, message)
        {
        }
        public SuccessDataResult(T data, List<string> messages) : base(data, true, false, messages)
        {
        }
        public SuccessDataResult(T data, List<Response> responses) : base(data, true, false, responses)
        {
        }

        public SuccessDataResult(T data) : base(data, true, false)
        {
        }

        public SuccessDataResult(string message) : base(default, true, false, message)
        {

        }

        public SuccessDataResult(List<string> messages) : base(default, true, false, messages)
        {

        }

        public SuccessDataResult(List<Response> responses) : base(default, true, false, responses)
        {

        }

        public SuccessDataResult() : base(default, true, false)
        {

        }
    }
}
