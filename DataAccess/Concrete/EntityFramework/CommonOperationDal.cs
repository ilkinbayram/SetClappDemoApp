using Core.DataAccess;
using Core.Entities.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Concrete.EntityFramework
{
    public class CommonOperationDal : ITransactionalOperation
    {
        private readonly ApplicationDbContext _context;
        public CommonOperationDal(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ComplexOperation(string userOperationClaimName, int userId)
        {
            return ExecuteTransaction(() =>
            {
                var myName = userOperationClaimName;

                var opCl = _context.OperationClaims.Add(new Core.Entities.Concrete.OperationClaim() { Name = "MyName" });

                if (_context.SaveChanges() == 0)
                    throw new Exception("");
            });
        }

        private bool ExecuteTransaction(Action action)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    action.Invoke();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("TODO:Message: Process couldn't be completed successfully till the end. All the changes are returned back.");
                    return false;
                }
            }
        }

        private async Task<bool> ExecuteTransactionAsync(Action action)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    action.Invoke();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("TODO:Message: Process couldn't be completed successfully till the end. All the changes are returned back.");
                    return false;
                }
            }
        }
    }
}
