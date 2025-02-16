using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterAsync(string fullName, string email, string password)
        {
            return await UserDAO.Instance.RegisterAsync(fullName, email, password);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await UserDAO.Instance.LoginAsync(email, password);
            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UserId", user.UserId.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("UserEmail", user.Email);
                _httpContextAccessor.HttpContext.Session.SetString("UserRole", user.Role.RoleName);
            }
            return user;
        }

        public async Task<bool> EditProfileAsync(int userId, string fullName, string phone)
        {
            return await UserDAO.Instance.EditProfileAsync(userId, fullName, phone);
        }

        public async Task LogoutAsync()
        {
            await Task.Run(() => { });
        }
    }
}

