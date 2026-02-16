using StoreManagement.Common.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.BAL.Interfaces
{
    public interface IAuthentication
    {
        Task<User> ValidateUser(string username, string password);
        Task<bool> RegisterUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> ApproveUser(int userId);
    }
}
