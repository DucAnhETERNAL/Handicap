using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly ICategoryRepository _categoryRepository;

    public IndexModel(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public List<Category> Categories { get; set; } = new List<Category>();

    public async Task OnGetAsync()
    {
        Categories = await _categoryRepository.GetAllCategoriesAsync();
    }
}
