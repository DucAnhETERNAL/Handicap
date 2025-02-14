using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<bool> EditProfileAsync(int userId, string fullName, string phone)
        {
            return await UserDAO.Instance.EditProfileAsync(userId, fullName, phone);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            return await UserDAO.Instance.LoginAsync(email, password);
        }

        public async Task<bool> RegisterAsync(string fullName, string email, string password)
        {
            return await UserDAO.Instance.RegisterAsync(fullName, email, password);
        }
    }
}
