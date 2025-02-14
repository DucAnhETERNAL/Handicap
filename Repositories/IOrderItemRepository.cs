using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        Task AddOrderItemAsync(int orderId, int productId, int quantity, decimal subtotal);
        Task<bool> DeleteOrderItemAsync(int orderItemId);

    }
}
