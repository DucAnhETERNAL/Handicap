using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ProductDetailModel : PageModel
{
    private readonly IProductRepository _productRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ICategoryRepository _categoryRepository;

    public Product Product { get; private set; }
    public List<Category> Categories { get; private set; } = new List<Category>();

    public ProductDetailModel(IProductRepository productRepository, ICartRepository cartRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _cartRepository = cartRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest("ID sản phẩm không hợp lệ.");
        }

        Categories = await _categoryRepository.GetAllCategoriesAsync();
        Product = await _productRepository.GetProductById(id);

        if (Product == null)
        {
            return NotFound("Sản phẩm không tồn tại.");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(int productId)
    {
        // Kiểm tra nếu người dùng chưa đăng nhập
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim))
        {
            Console.WriteLine("Không tìm thấy userId trong Claims. Chuyển hướng về đăng nhập.");
            return RedirectToPage("/Account/Login"); // Nếu user chưa đăng nhập, chuyển hướng về trang login
        }

        int loggedUserId = int.Parse(userIdClaim);
        Console.WriteLine($"Đã lấy được userId từ Claims: {loggedUserId}");

        if (productId <= 0)
        {
            Console.WriteLine("ID sản phẩm không hợp lệ.");
            return BadRequest("ID sản phẩm không hợp lệ.");
        }

        var product = await _productRepository.GetProductById(productId);
        if (product == null)
        {
            Console.WriteLine($"Không tìm thấy sản phẩm với ID {productId}.");
            return NotFound("Sản phẩm không tồn tại.");
        }

        Console.WriteLine($"Thêm sản phẩm ID {productId} vào giỏ hàng của User {loggedUserId}.");
        await _cartRepository.AddToCartAsync(loggedUserId, productId, 1);

        return RedirectToPage("/User/Cart");
    }

}
