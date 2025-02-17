using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Repositories;

namespace Handly.Pages.Seller
{
    public class ProductModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();
        public List<Product> Products { get; set; }
        public SelectList Categories { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Products = (await _productRepository.GetProductAll()).ToList();
            Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "CategoryId", "Name");
            if (id.HasValue)
            {
                Product = await _productRepository.GetProductById(id.Value) ?? new Product();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Product.ProductId == 0)
                await _productRepository.Add(Product);
            else
                await _productRepository.Update(Product);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _productRepository.Delete(id);
            return RedirectToPage();
        }
    }
}
