using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetPaymentsByUserAsync(int userId);
        Task<bool> ProcessPaymentAsync(int cartId, string paymentMethod, string transactionId);
        Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId);
    }
}
