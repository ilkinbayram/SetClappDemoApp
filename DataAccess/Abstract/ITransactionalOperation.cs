using Core.Entities.Abstract;

namespace Core.DataAccess
{
    public interface ITransactionalOperation
    {
        bool ComplexOperation(string userOperationClaimName, int userId);
    }
}
