using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class PaymentDAO : SingletonBase<PaymentDAO>
    {
        public async Task<List<Payment>> GetPaymentsByUserAsync(int userId)
        {
            return await _context.Payments
                .Where(p => p.Cart.UserId == userId)
                .Include(p => p.Cart)
                .ToListAsync();
        }

        public async Task<bool> ProcessPaymentAsync(int cartId, string paymentMethod, string transactionId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
            if (cart == null) return false;

            var payment = new Payment
            {
                CartId = cartId,
                PaymentMethod = paymentMethod,
                PaymentStatus = "Completed",
                TransactionId = transactionId
            };

            cart.Status = "Paid"; // Đánh dấu giỏ hàng đã thanh toán
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.TransactionId == transactionId);
        }
    }
}
