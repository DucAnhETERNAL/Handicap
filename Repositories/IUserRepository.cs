using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<bool> RegisterAsync(string fullName, string email, string password);
        Task<User?> LoginAsync(string email, string password);
        Task<bool> UpdateProfileAsync(int userId, string fullName, string email, string phone);
        Task LogoutAsync();
        Task<IEnumerable<User>> GetUserAll();
        Task<User>? GetUserByIdAsync(int id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(int id);
        Task<int> GetUserCount();
        Task<int> GetCustomerCount();
        Task<int> GetSellerCount();
        Task<int> GetShipperCount();

    }
}
