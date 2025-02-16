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

        public Task LogoutAsync()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> GetUserAll()
        {
            return await UserDAO.Instance.GetUserAll();
        }

        public async Task<User> GetUserById(int id)
        {
            return await UserDAO.Instance.GetUserById(id);
        }

        public async Task Add(User user)
        {
            await UserDAO.Instance.Add(user);
        }

        public async Task Update(User user)
        {
            await UserDAO.Instance.Update(user);
        }

        public async Task Delete(int id)
        {
            await UserDAO.Instance.Delete(id);
        }

        public async Task<int> GetUserCount()
        {
            return await UserDAO.Instance.GetUserCount();
        }

        public async Task<int> GetCustomerCount()
        {
            return await UserDAO.Instance.GetCustomerCount();
        }

        public async Task<int> GetSellerCount()
        {
            return await UserDAO.Instance.GetSellerCount();
        }

        public async Task<int> GetShipperCount()
        {
            return await UserDAO.Instance.GetShipperCount();
        }
    }
}

