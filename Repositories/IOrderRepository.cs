using Models;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersByUserAsync(int userId);
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<int> CreateOrderAsync(int userId, int cartId, decimal totalAmount);
    Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    Task<bool> AddOrderAsync(Order order);
    Task<Order?> GetLatestOrderByUserAsync(int userId);
}
