using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public async Task AddOrderItemAsync(int orderId, int productId, int quantity, decimal subtotal)
        {
            await OrderItemDAO.Instance.AddOrderItemAsync(orderId, productId, quantity, subtotal);
        }

        public async Task<bool> DeleteOrderItemAsync(int orderItemId)
        {
            return await OrderItemDAO.Instance.DeleteOrderItemAsync(orderItemId);
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await OrderItemDAO.Instance.GetOrderItemsByOrderIdAsync(orderId);
        }
    }
}
