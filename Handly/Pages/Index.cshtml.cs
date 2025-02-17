using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public IndexModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }


    public List<Product> Products { get; private set; }
    public List<Category> Categories { get; set; } = new List<Category>();

    public async Task OnGetAsync()
    {
        Products = (await _productRepository.GetProductAll()) as List<Product>;
        Categories = await _categoryRepository.GetAllCategoriesAsync();
    }



}
