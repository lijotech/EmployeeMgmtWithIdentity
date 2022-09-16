using Dapper;
using System.Data;

namespace EmployeeMgmt.Infrastructure
{
    public class DapperWrapper : IDapper
    {
        private readonly IDbConnection _dbConnection;

        public DapperWrapper(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null) =>
            _dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public interface IDapper
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);
    }
}
