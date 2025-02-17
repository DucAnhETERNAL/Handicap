using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId);
        Task<List<Payment>> GetPaymentsByUserAsync(int userId);
        Task<bool> ProcessPaymentAsync(int cartId, string paymentMethod, string transactionId);
        Task<string> CreatePaymentRequest(int userId, decimal amount);
    }
}

