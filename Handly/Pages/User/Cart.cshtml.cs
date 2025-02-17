using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

public class CartModel : PageModel
{
    private readonly ICartRepository _cartRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentService;

    public CartModel(ICartRepository cartRepository, ICategoryRepository categoryRepository,
                     IOrderRepository orderRepository, IPaymentRepository paymentService)
    {
        _cartRepository = cartRepository;
        _categoryRepository = categoryRepository;
        _orderRepository = orderRepository;
        _paymentService = paymentService;
    }

    public List<Cart> CartItems { get; private set; } = new List<Cart>();
    public List<Category> Categories { get; private set; } = new List<Category>();

    public async Task<IActionResult> OnGetAsync()
    {
        var userIdSession = HttpContext.Session.GetString("UserId");

        if (string.IsNullOrEmpty(userIdSession) || userIdSession == "0")
        {
            Console.WriteLine("Không tìm thấy userId trong session.");
            return RedirectToPage("/Account/Login");
        }

        int userId = int.Parse(userIdSession);
        Console.WriteLine($"Lấy user từ session thành công: UserId = {userId}");
        CartItems = await _cartRepository.GetUserCartAsync(userId);
        decimal total = CartItems.Sum(c => c.Product.Price * c.Quantity);

        Console.WriteLine($"Tổng tiền giỏ hàng: {total} VND");

        return Page();
    }





    public async Task<IActionResult> OnPostAsync(int id, int userId, int quantity = 1)
    {
        await _cartRepository.AddToCartAsync(userId, id, quantity);
        return RedirectToPage("/User/Cart", new { userId });
    }


    public async Task<IActionResult> OnPostCheckoutAsync(List<int> selectedItems)
    {
        var userIdSession = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdSession) || userIdSession == "0")
        {
            Console.WriteLine("Không tìm thấy userId khi thanh toán.");
            return RedirectToPage("/Account/Login");
        }

        int userId = int.Parse(userIdSession);
        Console.WriteLine($"Đã lấy userId từ session: {userId}");

        var selectedCarts = await _cartRepository.GetSelectedItemsAsync(selectedItems);
        decimal totalAmount = selectedCarts.Sum(c => c.Product.Price * c.Quantity);

        string paymentUrl = await _paymentService.CreatePaymentRequest(userId, totalAmount);
        Console.WriteLine($" Redirecting to PayOS: {paymentUrl}");

        if (string.IsNullOrEmpty(paymentUrl))
        {
            ModelState.AddModelError("", "Không thể tạo yêu cầu thanh toán.");
            return Page();
        }

        // Chỉ xóa giỏ hàng sau khi chuyển hướng thành công
        await _cartRepository.ClearCartAsync(userId);

        return Redirect(paymentUrl);
    }



    public async Task<IActionResult> OnPostIncreaseAsync(int id, int userId)
    {
        await _cartRepository.AddToCartAsync(userId, id, 1);
        return RedirectToPage("/User/Cart", new { userId });
    }

    public async Task<IActionResult> OnPostDecreaseAsync(int id, int userId)
    {
        await _cartRepository.DecreaseQuantityAsync(userId, id);
        return RedirectToPage("/User/Cart", new { userId });
    }


}
