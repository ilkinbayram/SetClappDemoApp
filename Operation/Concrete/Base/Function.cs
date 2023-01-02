using Core.Resources.OperationResources.Model;
using Core.Resources.OperationResources.Results;
using Operation.Abstract;

namespace Operation.Concrete.Base
{
    public class Function : IFunction
    {
        public virtual void DoExecute()
        {
        }

        public virtual Task DoExecuteAsync() => Task.CompletedTask;
    }


    public class Function<TOut> : IFunction<TOut>
        where TOut : class, IOperationResponse, new()
    {
        public virtual TOut DoExecute() => null;

        public virtual Task<TOut> DoExecuteAsync() => null;
        public virtual Task<TOut> DoExecuteAsync(string uri, DataMessage dataMover) => null;

        public virtual Task<TOut> DoExecuteAsync(IOperationModel model) => null;
    }
}
