using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Repositories;

namespace Handly.Pages.Seller
{
    public class CategoryModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [BindProperty]
        public Category Category { get; set; } = new Category();
        public List<Category> Categories { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Categories = await _categoryRepository.GetAllCategoriesAsync();
            if (id.HasValue)
            {
                Category = await _categoryRepository.GetCategoryByIdAsync(id.Value) ?? new Category();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Category.CategoryId == 0)
                await _categoryRepository.AddCategoryAsync(Category.CategoryName);
            else
                await _categoryRepository.UpdateCategoryAsync(Category.CategoryId, Category.CategoryName);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
            return RedirectToPage();
        }
    }
}
