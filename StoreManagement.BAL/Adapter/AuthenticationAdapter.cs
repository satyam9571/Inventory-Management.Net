using Dapper;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DapperContext;
using StoreManagement.Common.DTOs;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace StoreManagement.BAL.Adapter
{
    public class AuthenticationAdapter : IAuthentication
    {
        private readonly DbContext _context;

        public AuthenticationAdapter(DbContext context)
        {
            _context = context;
        }

        public async Task<User> ValidateUser(string username, string password)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM [users] WHERE [username] = @username AND [password_hash] = @password";
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { username, password });
            }
        }

        public async Task<bool> RegisterUser(User user)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"INSERT INTO [users] ([username], [password_hash], [full_name], [role], [is_approved], [created_at]) 
                              VALUES (@username, @password_hash, @full_name, @role, 0, GETDATE())";
                var result = await connection.ExecuteAsync(query, user);
                return result > 0;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM [users] ORDER BY created_at DESC";
                return await connection.QueryAsync<User>(query);
            }
        }

        public async Task<bool> ApproveUser(int userId)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "UPDATE [users] SET [is_approved] = 1 WHERE [id] = @userId";
                var result = await connection.ExecuteAsync(query, new { userId });
                return result > 0;
            }
        }
    }
}
