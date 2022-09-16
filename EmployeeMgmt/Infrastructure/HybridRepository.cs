
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace EmployeeMgmt.Infrastructure
{
    /// <summary>
    /// An <see cref="IRepository{T}"/> implementation that uses both entity framework for writes and updates and dapper for read.
    /// </summary>
    public abstract class HybridRepositoryBase<T> : HybridRepositoryBase<T, string>, IRepository<T> where T : class, IEntityBase
    {
        protected HybridRepositoryBase(DbContext dbContext, IDbConnection dbConnection, string tableName) : base(dbContext, dbConnection, tableName)
        {
        }

        protected HybridRepositoryBase(DbContext dbContext, IDbConnection dbConnection) : base(dbContext, dbConnection)
        {
        }

        public override Task<T> Add(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();
            return Task.FromResult(DbSet.Add(entity).Entity);
        }
    }

    public abstract class HybridRepositoryBase<T, TKey> : IRepository<T, TKey> where T : class, IEntityBase<TKey>
    {
        private readonly DbContext _dbContext;
        private readonly IDbConnection _dbConnection;
        private readonly string _tableName;
        private readonly DbSet<T> _dbSet;

        protected HybridRepositoryBase(DbContext dbContext,
                                       IDbConnection dbConnection,
                                       string tableName)
        {
            _dbContext = dbContext;
            _dbConnection = dbConnection;
            _tableName = tableName;
            _dbSet = _dbContext.Set<T>();
        }

        protected HybridRepositoryBase(DbContext dbContext,
                                       IDbConnection dbConnection)
            : this(dbContext, dbConnection, null)
        {
            var tableAttribute = typeof(T).GetCustomAttribute<TableAttribute>();
            if (tableAttribute == null)
                _tableName = typeof(T).Name;
            else
                _tableName = !string.IsNullOrEmpty(tableAttribute.Schema) ? $"[{tableAttribute.Schema}].[{tableAttribute.Name}]" : tableAttribute.Name;
        }

        protected virtual DbSet<T> DbSet => _dbSet;
        protected virtual DbContext DbContext => _dbContext;
        protected virtual IDbConnection DbConnection => _dbConnection;
        protected virtual string TableName => _tableName;

        /// <inheritdoc cref="IRepository{T}.AddRange"/>
        public virtual Task AddRange(params T[] entity)
        {
            _dbSet.AddRange(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="IRepository{T}.Add"/>
        public virtual Task<T> Add(T entity)
        {
            return Task.FromResult(_dbSet.Add(entity).Entity);
        }

        /// <inheritdoc cref="IRepository{T}.Update"/>
        public virtual Task<T> Update(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
            }
            catch
            {
                //Discard if already attached
            }

            return Task.FromResult(_dbSet.Update(entity).Entity);
        }

        /// <inheritdoc cref="IRepository{T}.UpdateRange"/>
        public virtual Task UpdateRange(params T[] entity)
        {
            _dbSet.UpdateRange(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="IRepository{T}.Remove"/>
        public virtual Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="IRepository{T}.RemoveRange"/>
        public virtual Task RemoveRange(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="IRepository{T}.Queryable"/>
        public virtual Task<IQueryable<T>> Queryable()
        {
            return Task.FromResult(_dbSet.AsQueryable());
        }

        /// <inheritdoc cref="IRepository{T}.GetEntity"/>
        public virtual Task<T> GetEntity(Expression<Func<T, bool>> filter)
        {
            return _dbSet.FirstOrDefaultAsync(filter);
        }

        public async Task<T> GetEntityById(TKey key)
        {
            return await _dbContext.FindAsync<T>(key);
        }

        /// <inheritdoc cref="IRepository{T}.GetAllAsync"/>
        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return _dbConnection.QueryAsync<T>($"SELECT * FROM {_tableName}");
        }

        public async Task<bool> Exists(TKey key)
        {
            return (await _dbContext.FindAsync<T>(key)) != null;
        }
    }
}
