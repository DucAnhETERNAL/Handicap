﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }

        [Required]
        public string Status { get; set; } = "Active"; // Active, Checked Out, Paid

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
