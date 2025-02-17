using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.Collections.Generic;
using System.Security.Claims;
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
    public string? Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userRepository.LoginAsync(Email, Password);
        if (user == null)
        {
            Message = "Sai email hoặc mật khẩu!";
            return Page();
        }

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role.RoleName)
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);

        // 🔹 Thêm session
        HttpContext.Session.SetString("UserId", user.UserId.ToString());
        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("UserRole", user.Role.RoleName);

        Console.WriteLine($"🟢 Session lưu UserId = {HttpContext.Session.GetString("UserId")}");

        return RedirectToPage("/Index");
    }

}
