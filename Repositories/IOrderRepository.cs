using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersByUserAsync(int userId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<int> CreateOrderAsync(int userId, int cartId, decimal totalAmount);
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);

    }
}
