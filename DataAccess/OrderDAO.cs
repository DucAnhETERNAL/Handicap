using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class OrderDAO : SingletonBase<OrderDAO>
    {
        public async Task<List<Order>> GetOrdersByUserAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.BuyerId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<int> CreateOrderAsync(int userId, int cartId, decimal totalAmount)
        {
            var order = new Order
            {
                BuyerId = userId,
                CartId = cartId,
                TotalAmount = totalAmount,
                Status = "Processing",
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Order?> GetLatestOrderByUserAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.BuyerId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
