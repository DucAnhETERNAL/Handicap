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

    public CartModel(ICartRepository cartRepository, ICategoryRepository categoryRepository)
    {
        _cartRepository = cartRepository;
        _categoryRepository = categoryRepository;
    }

    public List<Cart> CartItems { get; private set; } = new List<Cart>();
    public List<Category> Categories { get; private set; } = new List<Category>(); // Thêm thuộc tính Categories

    public async Task OnGetAsync(int userId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        CartItems = await _cartRepository.GetUserCartAsync(userId);
        Categories = await _categoryRepository.GetAllCategoriesAsync(); // Lấy danh sách categories
        if (string.IsNullOrEmpty(userIdClaim))
        {
            Console.WriteLine("🔴 Không tìm thấy userId khi lấy giỏ hàng.");
            return;
        }

        int loggedUserId = int.Parse(userIdClaim);
        Console.WriteLine($"🟢 Đang lấy giỏ hàng của User {loggedUserId}");
        CartItems = await _cartRepository.GetUserCartAsync(loggedUserId);
    }

    public async Task<IActionResult> OnPostAsync(int id, int userId, int quantity = 1)
    {
        await _cartRepository.AddToCartAsync(userId, id, quantity);
        return RedirectToPage("/User/Cart", new { userId });
    }
}
