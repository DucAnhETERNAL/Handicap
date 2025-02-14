using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int categoryId);
        Task<bool> AddCategoryAsync(string categoryName);
        Task<bool> UpdateCategoryAsync(int categoryId, string categoryName);
        Task<bool> DeleteCategoryAsync(int categoryId);

    }
}
