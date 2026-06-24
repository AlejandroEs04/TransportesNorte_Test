using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;

namespace ADN_Test.Data
{
    public class DataContextDapper
    {
        private readonly string _connectionString;

        public DataContextDapper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public async Task<IEnumerable<T>> Query<T>(string sql, object? parameters = null, CommandType? commandType = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return await db.QueryAsync<T>(sql, parameters, commandType: commandType);
        }

        public async Task<bool> Execute(string sql, object? parameters = null, CommandType? commandType = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return await db.ExecuteAsync(sql, parameters, commandType: commandType) > 0;
        }
    }
}
