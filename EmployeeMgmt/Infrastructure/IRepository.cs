using System.Linq.Expressions;

namespace EmployeeMgmt.Infrastructure
{
    public interface IRepository<T, TKey> where T : IEntityBase<TKey>
    {
        /// <summary>
        /// Add many entities in one call 
        /// </summary>
        Task AddRange(params T[] entities);

        /// <summary>
        /// Add a single entity
        /// </summary>
        Task<T> Add(T entity);

        /// <summary>
        /// Update an existing entity
        /// </summary>
        Task<T> Update(T entity);

        /// <summary>
        /// Update multiple existing entities in one call
        /// </summary>
        Task UpdateRange(params T[] entity);

        /// <summary>
        /// Hard delete an entity
        /// </summary>
        Task Remove(T entity);

        /// <summary>
        /// Hard delete multiple etities
        /// </summary>
        Task RemoveRange(params T[] entities);

        /// <summary>
        /// Returns an <see cref="IQueryable{T}"/> instance of the repository 
        /// </summary>
        Task<IQueryable<T>> Queryable();

        /// <summary>
        /// Returns a single entity based on a filter
        /// </summary>
        Task<T> GetEntity(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Returns a single entity based on a filter
        /// </summary>
        Task<T> GetEntityById(TKey key);

        /// <summary>
        /// Returns all entities in the repository without any filtration
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Checks if an entity exists in the data store
        /// </summary>
        /// <param name="key">The primary key for the entity</param>
        Task<bool> Exists(TKey key);
    }

    /// <summary>
    /// The repository definition used to access the data layer.
    /// All repository must implement this interface  
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public interface IRepository<T> : IRepository<T, string> where T : IEntityBase<string>
    {
    }

}
