using Core.Resources.OperationResources.Model;
using Core.Resources.OperationResources.Results;
using Operation.Abstract;

namespace Operation.Concrete.Base
{
    public class Operation<TFunction> : BaseOperation
        where TFunction : class, IFunction, new()
    {
        public override void Execute()
        {
            TFunction function = new TFunction();
            function.DoExecute();
            base.Execute();
        }

        public async override Task ExecuteAsync()
        {
            TFunction function = new TFunction();
            await function.DoExecuteAsync();
            await base.ExecuteAsync();
        }
    }

    public class Operation<TFunction, TResult> : BaseOperation<TResult>
        where TFunction : class, IFunction<TResult>, new()
        where TResult : class, IOperationResponse, new()
    {
        public override TResult Execute()
        {
            TFunction function = new TFunction();
            return function.DoExecute();
        }

        public async override Task<TResult> ExecuteAsync()
        {
            TFunction function = new TFunction();
            return await function.DoExecuteAsync();
        }

        public async override Task<TResult> ExecuteAsync(string uri, DataMessage dataMover)
        {
            TFunction function = new TFunction();
            return await function.DoExecuteAsync(uri, dataMover);
        }

        public async override Task<TResult> ExecuteAsync(IOperationModel model)
        {
            TFunction function = new TFunction();
            return await function.DoExecuteAsync(model);
        }
    }
}
