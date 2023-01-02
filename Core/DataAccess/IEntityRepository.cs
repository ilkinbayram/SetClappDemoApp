using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Core.Entities;
using Core.Entities.Abstract;

namespace Core.DataAccess
{
    /// <summary>
    /// This interface forces the concrete object to implement the methods of Database operations
    /// </summary>
    /// <typeparam name="T">This generic type is defined for the Database Entity</typeparam>
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        #region Synchronous Operations

        /// <summary>
        /// This method is used to commit the non-commited query on the database.
        /// </summary>
        /// <returns>This method will return the affected rows count.</returns>
        int Commit();

        /// <summary>
        /// This method is used to find the entity if's exist or not on the Database with the given id
        /// </summary>
        /// <param name="id">The unique primary key property of the related entity</param>
        /// <returns>This method will return the entity if it's exist. Otherwise will return NULL</returns>
        T? GetById(int id);

        /// <summary>
        /// This method is used to find the entity if's exist or not on the Database by the filter
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>This method will return the entity if it's exist. Otherwise will return NULL</returns>
        T? Get(Expression<Func<T, bool>> filter);

        /// <summary>
        /// This method is used to find the collection of the entity if it's exist or not on the Database by the filter
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>This method will return the collection of the entity if they're exist. Otherwise will return the empty list</returns>
        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// This method is used to add the entity to the Database.
        /// </summary>
        /// <param name="entity">The entity which will be created as new</param>
        /// <returns>This method will return the affected rows count</returns>
        int Add(T entity);

        /// <summary>
        /// This method is used to add the collection of the entitie to the Database.
        /// </summary>
        /// <param name="entities">The collection of the entities which will be created as new</param>
        /// <returns>This method will return the affected rows count</returns>
        int AddRange(List<T> entities);

        /// <summary>
        /// This method is used to update the exist entity to the Database.
        /// </summary>
        /// <param name="entity">The entity which will be updated instead of the exist entity</param>
        /// <returns>This method will return the affected rows count</returns>
        int Update(T entity);

        /// <summary>
        /// This method is used to update the exist entity to the Database.
        /// </summary>
        /// <param name="entities">The collection of the entities which will be updated instead of the exist collection of the entities</param>
        /// <returns>This method will return the affected rows count</returns>
        int UpdateRange(List<T> entities);

        /// <summary>
        /// This method is used to remove the given entity from the database if it's exist
        /// </summary>
        /// <param name="entity">The entity which will be deleted from the database when this method is executed successfully</param>
        /// <returns>This method will return the affected rows count</returns>
        int Remove(T entity);

        /// <summary>
        /// This method is used to remove the given collection of the entities from the database if they're exist
        /// </summary>
        /// <param name="entities">The collection of the exist entities which will be deleted from the database when this method is executed successfully</param>
        /// <returns>This method will return the affected rows count</returns>
        int RemoveRange(List<T> entities);

        /// <summary>
        /// This method is used to update the status of the entity. Then it will behave as deleted item.
        /// </summary>
        /// <param name="entity">The entity which will be updated with its status</param>
        /// <returns>This method will return the affected rows count</returns>
        int RemoveByStatus(T entity);

        /// <summary>
        /// This method is used to update the status of the collection of the enties. Then they will behave as deleted items.
        /// </summary>
        /// <param name="entities">The entities which will be updated with their status</param>
        /// <returns>This method will return the affected rows count</returns>
        int RemoveRangeByStatus(List<T> entities);


        /// <summary>
        /// This method will return the count of the entities.
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>Returns the count of found dataes</returns>
        int GetCount(Expression<Func<T, bool>> filter = null);
        #endregion

        #region Asynchronous Operations

        /// <summary>
        /// This method is used to asynchronously commit the non-commited query on the database in the same time.
        /// </summary>
        /// <returns>This method will return the affected rows count.</returns>
        Task<int> CommitAsync();

        /// <summary>
        /// This method is used to asynchronously find the entity if's exist or not on the Database with the given id.
        /// </summary>
        /// <param name="id">The unique primary key property of the related entity</param>
        /// <returns>This method will return the entity if it's exist. Otherwise will return NULL</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// This method is used to asynchronously find the entity if's exist or not on the Database by the filter
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>This method will return the entity if it's exist. Otherwise will return NULL</returns>
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// This method is used to asynchronously find the collection of the entity if it's exist or not on the Database by the filter
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>This method will return the collection of the entity if they're exist. Otherwise will return the empty list</returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// This method is used to asynchronously add the entity to the Database in the same time.
        /// </summary>
        /// <param name="entity">The entity which will be created as new</param>
        /// <returns>This method will return the affected rows count</returns>
        Task<int> AddAsync(T entity);

        /// <summary>
        /// This method is used to asynchronously add the collection of the entitie to the Database in the same time.
        /// </summary>
        /// <param name="entities">The collection of the entities which will be created as new</param>
        /// <returns>This method will return the affected rows count</returns>
        Task<int> AddRangeAsync(List<T> entities);

        /// <summary>
        /// This method is used to asynchronously update the exist entity to the Database in the same time.
        /// </summary>
        /// <param name="entity">The entity which will be updated instead of the exist entity</param>
        /// <returns>This method will return the affected rows count</returns>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// This method is used to asynchronously update the exist entity to the Database in the same time.
        /// </summary>
        /// <param name="entities">The collection of the entities which will be updated instead of the exist collection of the entities</param>
        /// <returns>This method will return the affected rows count</returns>
        Task<int> UpdateRangeAsync(List<T> entities);

        /// <summary>
        /// This method is used to asynchronously remove the given entity from the database if it's exist in the same time.
        /// </summary>
        /// <param name="entity">The entity which will be deleted from the database when this method is executed successfully</param>
        /// <returns>This method will return the affected rows count</returns>
        Task<int> RemoveAsync(T entity);

        /// <summary>
        /// This method is used to asynchronously remove the given collection of the entities from the database if they're exist in the same time.
        /// </summary>
        /// <param name="entities">The collection of the exist entities which will be deleted from the database when this method is executed successfully</param>
        /// <returns>This method will return the affected rows count</returns>
        Task<int> RemoveRangeAsync(List<T> entities);

        /// <summary>
        /// This method is used to asynchronously update the status of the entity in the same time. Then it will behave as deleted item.
        /// </summary>
        /// <param name="entity">The entity which will be updated with its status</param>
        /// <returns>This method will return the affected rows count</returns>
        Task<int> RemoveByStatusAsync(T entity);

        /// <summary>
        /// This method is used to asynchronously update the status of the collection of the enties. Then they will behave as deleted items in the same time.
        /// </summary>
        /// <param name="entities">The entities which will be updated with their status</param>
        /// <returns>This method will return the affected rows count</returns>
        Task<int> RemoveRangeByStatusAsync(List<T> entities);

        /// <summary>
        /// This method will return the count of the entities.
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>Returns the count of found dataes</returns>
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);
        #endregion
    }
}
