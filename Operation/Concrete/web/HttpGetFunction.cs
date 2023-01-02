using Core.Resources.OperationResources.Results;
using Operation.Concrete.Base;

namespace web.Operation.Concrete
{
    public class HttpGetFunction<TResult> : Function<TResult>
        where TResult : class, IOperationResponse, new()
    {
        public override async Task<TResult> DoExecuteAsync()
        {
            return null;
        }
    }
}
