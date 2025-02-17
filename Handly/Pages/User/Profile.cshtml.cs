using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Repositories;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
public class ProfileModel : PageModel
{
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProfileModel(IUserRepository userRepository, ICategoryRepository categoryRepository)
    {
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
    }

    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public List<Category> Categories { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return RedirectToPage("/Account/Login");

        FullName = user.FullName;
        Email = user.Email;
        Phone = user.Phone ?? "N/A";

        Categories = await _categoryRepository.GetAllCategoriesAsync();

        return Page();
    }
}
