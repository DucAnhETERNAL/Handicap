using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Repositories;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize] // Chỉ cho phép user đã đăng nhập
public class EditProfileModel : PageModel
{
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;

    public EditProfileModel(IUserRepository userRepository, ICategoryRepository categoryRepository)
    {
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
    }

    [BindProperty] public string FullName { get; set; } = string.Empty;
    [BindProperty] public string Email { get; set; } = string.Empty;
    [BindProperty] public string Phone { get; set; } = string.Empty;
    public List<Category> Categories { get; set; } = new();
    [BindProperty] public string? Message { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return RedirectToPage("/Account/Login");

        FullName = user.FullName;
        Email = user.Email;
        Phone = user.Phone ?? "";

        // ✅ **Lấy danh sách Categories**
        Categories = await _categoryRepository.GetAllCategoriesAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        bool updated = await _userRepository.UpdateProfileAsync(userId, FullName, Email, Phone);
        if (!updated)
        {
            Message = "Email đã tồn tại hoặc cập nhật thất bại!";
            return Page();
        }

        Message = "Cập nhật thành công!";
        return RedirectToPage("/User/Profile");
    }
}
