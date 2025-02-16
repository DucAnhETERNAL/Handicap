using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductsModel : PageModel
{
    private readonly IProductRepository _productRepository;

    public ProductsModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<Product> Products { get; set; } = new List<Product>();

    public async Task OnGetAsync()
    {
        Products = (await _productRepository.GetProductAll()).ToList();
    }
}
