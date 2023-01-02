using System.Collections.Generic;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        private readonly T _data;
        public DataResult(T data, bool isSuccess, bool isProcessBroken) : base(isSuccess, isProcessBroken)
        {
            _data = data;
        }

        public DataResult(T data, bool isSuccess, bool isProcessBroken, string message) : base(isSuccess, isProcessBroken, message)
        {
            _data = data;
        }

        public DataResult(T data, bool isSuccess, bool isProcessBroken, string message, string fullDetail) : base(isSuccess, isProcessBroken, message, fullDetail)
        {
            _data = data;
        }

        public DataResult(T data, bool isSuccess, bool isProcessBroken, List<string> messages) : base(isSuccess, isProcessBroken, messages)
        {
            _data = data;
        }

        public DataResult(T data, bool isSuccess, bool isProcessBroken, List<Response> responses):base(isSuccess, isProcessBroken, responses)
        {
            _data = data;
        }

        public T Data => _data;
    }
}
