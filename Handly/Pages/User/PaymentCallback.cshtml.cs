using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DataAccess;
using Models;

public class PaymentCallbackModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public PaymentCallbackModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public string Status { get; private set; } = "failed";

    public async Task<IActionResult> OnGetAsync(string orderId, string status)
    {
        Console.WriteLine($"🔹 PayOS Callback: OrderId = {orderId}, Status = {status}");

        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            Console.WriteLine($"❌ Không tìm thấy đơn hàng với ID: {orderId}");
            return Page();
        }

        if (status == "success")
        {
            order.Status = "Completed";
            await _context.SaveChangesAsync();
            Console.WriteLine($"✅ Thanh toán thành công, cập nhật đơn hàng ID {orderId}");
            Status = "success";
        }
        else
        {
            order.Status = "Failed";
            await _context.SaveChangesAsync();
            Console.WriteLine($"❌ Thanh toán thất bại, cập nhật đơn hàng ID {orderId}");
        }

        return Page();
    }
}
