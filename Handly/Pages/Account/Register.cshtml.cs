using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.Threading.Tasks;

public class RegisterModel : PageModel
{
    private readonly IUserRepository _userRepository;

    public RegisterModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [BindProperty]
    public string FullName { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string? Message { get; set; } // Lưu thông báo lỗi để hiển thị popup

    public async Task<IActionResult> OnPostAsync()
    {
        bool success = await _userRepository.RegisterAsync(FullName, Email, Password);
        if (!success)
        {
            Message = "Email đã tồn tại.";
            return Page();
        }
        return RedirectToPage("/Account/Login");
    }
}
