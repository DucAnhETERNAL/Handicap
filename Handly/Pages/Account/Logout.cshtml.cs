using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.Threading.Tasks;

public class LogoutModel : PageModel
{
    private readonly IUserRepository _userRepository;

    public LogoutModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task OnGetAsync()
    {
        await _userRepository.LogoutAsync();
        Response.Redirect("/Account/Login");
    }
}
