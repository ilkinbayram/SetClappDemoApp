using Core.Resources.OperationResources.Model;
using Core.Resources.OperationResources.Results;
using Operation.Abstract;

namespace Operation.Concrete.Base
{
    public abstract class BaseOperation : IBaseOperation
    {
        public virtual void Execute()
        {
        }

        public virtual Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }

        protected private void Proceed()
        {
        }
    }

    public abstract class BaseOperation<TResult> : IBaseOperation
        where TResult : class, IOperationResponse, new()
    {
        public virtual TResult Execute() => null;
        public virtual Task<TResult> ExecuteAsync() => null;
        public virtual Task<TResult> ExecuteAsync(string uri, DataMessage dataMover) => null;
        public virtual Task<TResult> ExecuteAsync(IOperationModel model) => null;
        protected private async Task ProceedAsync()
        {
            await Task.CompletedTask;
        }
    }
}
