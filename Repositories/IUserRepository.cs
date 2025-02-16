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
        Task<bool> EditProfileAsync(int userId, string fullName, string phone);
        Task LogoutAsync();
    }
}
