using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<List<Order>> GetOrdersByUserAsync(int userId)
        {
            return await OrderDAO.Instance.GetOrdersByUserAsync(userId);
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await OrderDAO.Instance.GetOrderByIdAsync(orderId);
        }

        public async Task<int> CreateOrderAsync(int userId, int cartId, decimal totalAmount)
        {
            return await OrderDAO.Instance.CreateOrderAsync(userId, cartId, totalAmount);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            return await OrderDAO.Instance.UpdateOrderStatusAsync(orderId, status);
        }

        public async Task<bool> AddOrderAsync(Order order)
        {
            return await OrderDAO.Instance.AddOrderAsync(order);
        }

        public async Task<Order?> GetLatestOrderByUserAsync(int userId)
        {
            return await OrderDAO.Instance.GetLatestOrderByUserAsync(userId);
        }
    }

}
