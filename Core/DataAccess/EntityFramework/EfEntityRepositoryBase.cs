using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>, IEntityQueryableRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext
    {
        protected readonly TContext Context;

        public EfEntityRepositoryBase(TContext applicationContext)
        {
            Context = applicationContext;
        }

        ~EfEntityRepositoryBase()
        {
            Context.Dispose();
        }

        #region Synchronous Operations

        /// <summary>
        /// This method is used to commit the non-commited query on the database.
        /// </summary>
        /// <returns>This method will return the affected rows count.</returns>
        public int Commit()
        {
            return Context.SaveChanges();
        }

        /// <summary>
        /// This method is used to find the entity if's exist or not on the Database with the given id
        /// </summary>
        /// <param name="id">The unique primary key property of the related entity</param>
        /// <returns>This method will return the entity if it's exist. Otherwise will return NULL</returns>
        public virtual TEntity? GetById(int id)
        {
            return Context.Set<TEntity>().Where(x => x.IsActive == true).FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// This method is used to find the entity if's exist or not on the Database by the filter
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>This method will return the entity if it's exist. Otherwise will return NULL</returns>
        public virtual TEntity? Get(Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>().Where(x => x.IsActive == true).FirstOrDefault(filter);
        }

        /// <summary>
        /// This method is used to find the collection of the entity if it's exist or not on the Database by the filter
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>This method will return the collection of the entity if they're exist. Otherwise will return the empty list</returns>
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? Context.Set<TEntity>().Where(x => x.IsActive == true).ToList()
                : Context.Set<TEntity>().Where(x => x.IsActive == true).Where(filter).ToList();
        }

        /// <summary>
        /// This method is used to add the entity to the Database.
        /// </summary>
        /// <param name="entity">The entity which will be created as new</param>
        /// <returns>This method will return the affected rows count</returns>
        public virtual int Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return Context.SaveChanges();
        }

        /// <summary>
        /// This method is used to add the collection of the entitie to the Database.
        /// </summary>
        /// <param name="entities">The collection of the entities which will be created as new</param>
        /// <returns>This method will return the affected rows count</returns>
        public virtual int AddRange(List<TEntity> entities)
        {
            using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    int affectedRows;
                    Context.Set<TEntity>().AddRange(entities);
                    affectedRows = Context.SaveChanges();
                    if (entities.Count != affectedRows)
                    {
                        transaction.Rollback();
                        return default;
                    }
                    transaction.Commit();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("TODO:Message: Process couldn't be completed successfully till the end. All the changes are returned back.");
                    return default;
                }
            }
        }

        /// <summary>
        /// This method is used to update the exist entity to the Database.
        /// </summary>
        /// <param name="entity">The entity which will be updated instead of the exist entity</param>
        /// <returns>This method will return the affected rows count</returns>
        public virtual int Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return Context.SaveChanges();
        }

        /// <summary>
        /// This method is used to update the exist entity to the Database.
        /// </summary>
        /// <param name="entities">The collection of the entities which will be updated instead of the exist collection of the entities</param>
        /// <returns>This method will return the affected rows count</returns>
        public virtual int UpdateRange(List<TEntity> entities)
        {
            using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    int affectedRows;
                    Context.Set<TEntity>().UpdateRange(entities);
                    affectedRows = Context.SaveChanges();
                    if (entities.Count != affectedRows)
                    {
                        transaction.Rollback();
                        return default;
                    }
                    transaction.Commit();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("TODO:Message: Process couldn't be completed successfully till the end. All the changes are returned back.");
                    return default;
                }
            }
        }

        /// <summary>
        /// This method is used to remove the given entity from the database if it's exist
        /// </summary>
        /// <param name="entity">The entity which will be deleted from the database when this method is executed successfully</param>
        /// <returns>This method will return the affected rows count</returns>
        public virtual int Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Context.SaveChanges();
        }

        /// <summary>
        /// This method is used to remove the given collection of the entities from the database if they're exist
        /// </summary>
        /// <param name="entities">The collection of the exist entities which will be deleted from the database when this method is executed successfully</param>
        /// <returns>This method will return the affected rows count</returns>
        public virtual int RemoveRange(List<TEntity> entities)
        {
            using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    int affectedRows;
                    Context.Set<TEntity>().RemoveRange(entities);
                    affectedRows = Context.SaveChanges();
                    if (entities.Count != affectedRows)
                    {
                        transaction.Rollback();
                        return default;
                    }
                    transaction.Commit();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("TODO:Message: Process couldn't be completed successfully till the end. All the changes are returned back.");
                    return default;
                }
            }
        }

        /// <summary>
        /// This method is used to update the status of the entity. Then it will behave as deleted item.
        /// </summary>
        /// <param name="entity">The entity which will be updated with its status</param>
        /// <returns>This method will return the affected rows count</returns>
        public virtual int RemoveByStatus(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return Context.SaveChanges();
        }

        /// <summary>
        /// This method is used to update the status of the collection of the enties. Then they will behave as deleted items.
        /// </summary>
        /// <param name="entities">The entities which will be updated with their status</param>
        /// <returns>This method will return the affected rows count</returns>
        public virtual int RemoveRangeByStatus(List<TEntity> entities)
        {
            using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    int affectedRows;
                    Context.Set<TEntity>().UpdateRange(entities);
                    affectedRows = Context.SaveChanges();
                    if (entities.Count != affectedRows)
                    {
                        transaction.Rollback();
                        return default;
                    }
                    transaction.Commit();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("TODO:Message: Process couldn't be completed successfully till the end. All the changes are returned back.");
                    return default;
                }
            }
        }

        /// <summary>
        /// This method will return the count of the entities.
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>Returns the count of found dataes</returns>
        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
    ? Context.Set<TEntity>().Where(x => x.IsActive == true).Count()
    : Context.Set<TEntity>().Where(x => x.IsActive == true).Count(filter);
        }

        #endregion


        #region Asynchronous Operations

        /// <summary>
        /// This method is used to asynchronously commit the non-commited query on the database in the same time.
        /// </summary>
        /// <returns>This method will return the affected rows count.</returns>
        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// This method is used to asynchronously find the entity if's exist or not on the Database with the given id.
        /// </summary>
        /// <param name="id">The unique primary key property of the related entity</param>
        /// <returns>This method will return the entity if it's exist. Otherwise will return NULL</returns>
        public async virtual Task<TEntity?> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().Where(x => x.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// This method is used to asynchronously find the entity if's exist or not on the Database by the filter
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>This method will return the entity if it's exist. Otherwise will return NULL</returns>
        public async virtual Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Context.Set<TEntity>().Where(x => x.IsActive == true).FirstOrDefaultAsync(filter);
        }

        /// <summary>
        /// This method is used to asynchronously find the collection of the entity if it's exist or not on the Database by the filter
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>This method will return the collection of the entity if they're exist. Otherwise will return the empty list</returns>
        public async virtual Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var result = filter != null
                ? Context.Set<TEntity>().Where(x => x.IsActive == true).Where(filter).ToListAsync()
                : Context.Set<TEntity>().Where(x => x.IsActive == true).ToListAsync();
            return await result;
        }

        /// <summary>
        /// This method is used to asynchronously add the entity to the Database in the same time.
        /// </summary>
        /// <param name="entity">The entity which will be created as new</param>
        /// <returns>This method will return the affected rows count</returns>
        public async virtual Task<int> AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// This method is used to asynchronously add the collection of the entitie to the Database in the same time.
        /// </summary>
        /// <param name="entities">The collection of the entities which will be created as new</param>
        /// <returns>This method will return the affected rows count</returns>
        public async virtual Task<int> AddRangeAsync(List<TEntity> entities)
        {
            using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    int affectedRows;
                    await Context.Set<TEntity>().AddRangeAsync(entities);
                    affectedRows = await Context.SaveChangesAsync();
                    if (entities.Count != affectedRows)
                    {
                        await transaction.RollbackAsync();
                        return default;
                    }
                    await transaction.CommitAsync();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("TODO:Message: Process couldn't be completed successfully till the end. All the changes are returned back.");
                    return default;
                }
            }
        }

        /// <summary>
        /// This method is used to asynchronously update the exist entity to the Database in the same time.
        /// </summary>
        /// <param name="entity">The entity which will be updated instead of the exist entity</param>
        /// <returns>This method will return the affected rows count</returns>
        public async virtual Task<int> UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// This method is used to asynchronously update the exist entity to the Database in the same time.
        /// </summary>
        /// <param name="entities">The collection of the entities which will be updated instead of the exist collection of the entities</param>
        /// <returns>This method will return the affected rows count</returns>
        public async virtual Task<int> UpdateRangeAsync(List<TEntity> entities)
        {
            using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    int affectedRows;
                    Context.Set<TEntity>().UpdateRange(entities);
                    affectedRows = await Context.SaveChangesAsync();
                    if (entities.Count != affectedRows)
                    {
                        await transaction.RollbackAsync();
                        return default;
                    }
                    await transaction.CommitAsync();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("TODO:Message: Process couldn't be completed successfully till the end. All the changes are returned back.");
                    return default;
                }
            }
        }

        /// <summary>
        /// This method is used to asynchronously remove the given entity from the database if it's exist in the same time.
        /// </summary>
        /// <param name="entity">The entity which will be deleted from the database when this method is executed successfully</param>
        /// <returns>This method will return the affected rows count</returns>
        public async virtual Task<int> RemoveAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// This method is used to asynchronously remove the given collection of the entities from the database if they're exist in the same time.
        /// </summary>
        /// <param name="entities">The collection of the exist entities which will be deleted from the database when this method is executed successfully</param>
        /// <returns>This method will return the affected rows count</returns>
        public async virtual Task<int> RemoveRangeAsync(List<TEntity> entities)
        {
            using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    int affectedRows;
                    Context.Set<TEntity>().RemoveRange(entities);
                    affectedRows = await Context.SaveChangesAsync();
                    if (entities.Count != affectedRows)
                    {
                        await transaction.RollbackAsync();
                        return default;
                    }
                    await transaction.CommitAsync();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("TODO:Message: Process couldn't be completed successfully till the end. All the changes are returned back.");
                    return default;
                }
            }
        }

        /// <summary>
        /// This method is used to asynchronously update the status of the entity in the same time. Then it will behave as deleted item.
        /// </summary>
        /// <param name="entity">The entity which will be updated with its status</param>
        /// <returns>This method will return the affected rows count</returns>
        public async virtual Task<int> RemoveByStatusAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// This method is used to asynchronously update the status of the collection of the enties. Then they will behave as deleted items in the same time.
        /// </summary>
        /// <param name="entities">The entities which will be updated with their status</param>
        /// <returns>This method will return the affected rows count</returns>
        public async virtual Task<int> RemoveRangeByStatusAsync(List<TEntity> entities)
        {
            using (IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    int affectedRows;
                    Context.Set<TEntity>().UpdateRange(entities);
                    affectedRows = await Context.SaveChangesAsync();
                    if (entities.Count != affectedRows)
                    {
                        await transaction.RollbackAsync();
                        return default;
                    }
                    await transaction.CommitAsync();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("TODO:Message: Process couldn't be completed successfully till the end. All the changes are returned back.");
                    return default;
                }
            }
        }

        /// <summary>
        /// This method will return the count of the entities.
        /// </summary>
        /// <param name="filter">The dynamic expression filter to find the data</param>
        /// <returns>Returns the count of found dataes</returns>
        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var result = filter == null
? Context.Set<TEntity>().Where(x => x.IsActive == true).CountAsync()
: Context.Set<TEntity>().Where(x => x.IsActive == true).CountAsync(filter);
            return await result;
        }
        #endregion


        /// <summary>
        /// Creates the query as Queryable
        /// </summary>
        /// <param name="expression">This parameter is used to filter the dataes as LINQ query</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null)
        {
            var result = expression == null ?
                Context.Set<TEntity>() :
                Context.Set<TEntity>().Where(expression);
            return result;
        }



    }
}
