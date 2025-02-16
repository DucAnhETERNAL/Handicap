using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SearchResultModel : PageModel
{
    private readonly IProductRepository _productRepository;

    public SearchResultModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<Product> Products { get; set; } = new List<Product>();
    public string SearchQuery { get; set; } = string.Empty;

    public async Task OnGetAsync(string? searchQuery)
    {
        if (!string.IsNullOrEmpty(searchQuery))
        {
            SearchQuery = searchQuery;
            Products = (await _productRepository.SearchProductsAsync(searchQuery)).ToList();
        }
    }
}
