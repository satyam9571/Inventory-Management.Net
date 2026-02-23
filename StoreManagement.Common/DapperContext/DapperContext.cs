using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace StoreManagement.Common.DapperContext
{
    public class DbContext
    {
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task ExecuteAsync(string sql, object param = null)
        {
            using (var con = CreateConnection())
            {
                await con.ExecuteAsync(sql, param);   
            }
        }

        public async Task<List<T>> QueryAsync<T>(string sql, object param = null)
        {
            using (var con = CreateConnection())
            {
                var data = await con.QueryAsync<T>(sql, param); 
                return data.ToList();
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null)
        {
            using (var con = CreateConnection())
            {
                return await con.QueryFirstOrDefaultAsync<T>(sql, param);
            }
        }
    }
}
