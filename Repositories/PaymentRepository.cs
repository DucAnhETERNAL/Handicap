using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId)
        {
            return await PaymentDAO.Instance.GetPaymentByTransactionIdAsync(transactionId);
        }

        public async Task<List<Payment>> GetPaymentsByUserAsync(int userId)
        {
            return await PaymentDAO.Instance.GetPaymentsByUserAsync(userId);
        }

        public async Task<bool> ProcessPaymentAsync(int cartId, string paymentMethod, string transactionId)
        {
            return await PaymentDAO.Instance.ProcessPaymentAsync(cartId, paymentMethod, transactionId);
        }
    }
}
