using Core.Resources.OperationResources.Model;
using Core.Resources.OperationResources.Results;

namespace Operation.Abstract
{
    public interface IFunction
    {
        void DoExecute();
        Task DoExecuteAsync();
    }

    public interface IFunction<TResult> where TResult : class, IOperationResponse, new()
    {
        TResult DoExecute();
        Task<TResult> DoExecuteAsync();
        Task<TResult> DoExecuteAsync(string uri, DataMessage dataMover);

        Task<TResult> DoExecuteAsync(IOperationModel model);
    }
}
