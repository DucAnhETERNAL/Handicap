using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class UserDAO : SingletonBase<UserDAO>
    {
        public async Task<bool> RegisterAsync(string fullName, string email, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return false; // Email đã tồn tại

            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == 2);
            if (userRole == null) return false;

            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = password,
                RoleId = userRole.RoleId,
                Role = userRole,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<bool> EditProfileAsync(int userId, string fullName, string phone)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.FullName = fullName;
            user.Phone = phone;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
