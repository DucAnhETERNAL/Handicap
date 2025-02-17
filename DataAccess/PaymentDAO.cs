using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class PaymentDAO : SingletonBase<PaymentDAO>
    {
        private readonly ApplicationDbContext _context;

        public PaymentDAO()
        {
            _context = new ApplicationDbContext(); // Khởi tạo DbContext
        }

        // ✅ Lấy danh sách thanh toán của người dùng
        public async Task<List<Payment>> GetPaymentsByUserAsync(int userId)
        {
            return await _context.Payments
                .Where(p => _context.Carts.Any(c => c.CartId == p.CartId && c.UserId == userId))
                .Include(p => p.Cart) // Load Cart nếu cần thiết
                .ToListAsync();
        }

        // ✅ Xử lý thanh toán & cập nhật trạng thái giỏ hàng mà không cần `Payments`
        public async Task<bool> ProcessPaymentAsync(int cartId, string paymentMethod, string transactionId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
            if (cart == null) return false; // Kiểm tra xem giỏ hàng có tồn tại không

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

        // ✅ Lấy thông tin thanh toán bằng transactionId
        public async Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId)
        {
            return await _context.Payments
                .Include(p => p.Cart) // Load Cart nếu cần
                .FirstOrDefaultAsync(p => p.TransactionId == transactionId);
        }
    }
}
