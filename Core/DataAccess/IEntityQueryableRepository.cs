using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Core.Entities;
using Core.Entities.Abstract;

namespace Core.DataAccess
{
    /// <summary>
    /// This interface forces the concrete object to implement the method which returns IQueryable response
    /// </summary>
    /// <typeparam name="T">This generic type is defined for the Database Entity</typeparam>
    public interface IEntityQueryableRepository<T> 
        where T: class, IEntity, new() 
    {
        /// <summary>
        /// Creates the query as Queryable
        /// </summary>
        /// <param name="expression">This parameter is used to filter the dataes as LINQ query</param>
        /// <returns>This method will return the generated query for the entity.</returns>
        IQueryable<T> Query(Expression<Func<T, bool>> expression = null);
    }
}
