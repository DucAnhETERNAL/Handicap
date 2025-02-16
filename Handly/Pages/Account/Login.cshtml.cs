using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.Threading.Tasks;

public class LoginModel : PageModel
{
    private readonly IUserRepository _userRepository;

    public LoginModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string? Message { get; set; } // Lưu thông báo lỗi để hiển thị popup

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userRepository.LoginAsync(Email, Password);
        if (user == null)
        {
            Message = "Sai email hoặc mật khẩu!";
            return Page();
        }
        return RedirectToPage("/Index");
    }
}
