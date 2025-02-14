using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public int BuyerId { get; set; }
        public User? Buyer { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; } // Đơn hàng được tạo từ giỏ hàng nào?
        public Cart? Cart { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        public string Status { get; set; } = "Processing"; // Processing, Shipped, Delivered, Cancelled

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
