using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class UserDAO : SingletonBase<UserDAO>
    {

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }
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
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                Console.WriteLine($"Đăng nhập thất bại: Không tìm thấy user với email {email}");
                return null;
            }

            Console.WriteLine($"Đăng nhập thành công: UserId = {user.UserId}, Role = {user.Role.RoleName}");
            return user;
        }


        public async Task<bool> UpdateProfileAsync(int userId, string fullName, string email, string phone)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null) return false;

            // Kiểm tra nếu email đã tồn tại trong hệ thống nhưng không phải của user hiện tại
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.UserId != userId);
            if (existingUser != null) return false;

            user.FullName = fullName;
            user.Email = email;
            user.Phone = phone;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<User>> GetUserAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users
                                      .Include(u => u.Orders)
                                      .FirstOrDefaultAsync(u => u.UserId == id);

            return user;
        }


        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            var existingItem = await GetUserById(user.UserId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(user);
            }
            else
            {
                _context.Users.Add(user);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetUserCount()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetSellerCount()
        {
            return await _context.Users.CountAsync(u => u.RoleId == 1);
        }

        public async Task<int> GetCustomerCount()
        {
            return await _context.Users.CountAsync(u => u.RoleId == 2);
        }
        public async Task<int> GetShipperCount()
        {
            return await _context.Users.CountAsync(u => u.RoleId == 3);
        }

    }
}

