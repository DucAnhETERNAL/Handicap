using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class CartDAO : SingletonBase<CartDAO>
    {
        public async Task<List<Cart>> GetUserCartAsync(int userId)
        {
            if (userId <= 0)
            {
                Console.WriteLine("userId không hợp lệ khi lấy giỏ hàng.");
                return new List<Cart>();
            }

            var cartItems = await _context.Carts
                .Where(c => c.UserId == userId && c.Status == "Active")
                .Include(c => c.Product)
                .ToListAsync();

            Console.WriteLine($"Lấy giỏ hàng thành công: {cartItems.Count} sản phẩm cho User {userId}");

            return cartItems;
        }


        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            if (userId <= 0 || productId <= 0 || quantity <= 0)
            {
                Console.WriteLine("Lỗi: userId hoặc productId không hợp lệ.");
                return;
            }

            var existingCartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId && c.Status == "Active");

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                Console.WriteLine($"Đã cập nhật số lượng sản phẩm ID {productId} trong giỏ hàng của User {userId}.");
            }
            else
            {
                var cartItem = new Cart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    Status = "Active"
                };
                _context.Carts.Add(cartItem);
                Console.WriteLine($"Đã thêm sản phẩm mới ID {productId} vào giỏ hàng của User {userId}.");
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(int cartId)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(int userId)
        {
            var cartItems = await _context.Carts.Where(c => c.UserId == userId).ToListAsync();
            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCartItemAsync(Cart cartItem)
        {
            _context.Carts.Update(cartItem);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Cart>> GetSelectedItemsAsync(List<int> selectedItemIds)
        {
            return await _context.Carts
                .Where(c => selectedItemIds.Contains(c.CartId))
                .Include(c => c.Product)
                .ToListAsync();
        }
    }
}
