using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<bool> AddCategoryAsync(string categoryName)
        {
            return await CategoryDAO.Instance.AddCategoryAsync(categoryName);
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            return await CategoryDAO.Instance.DeleteCategoryAsync(categoryId);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await CategoryDAO.Instance.GetAllCategoriesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await CategoryDAO.Instance.GetCategoryByIdAsync(categoryId);
        }

        public async Task<int> GetCategoryCount()
        {
            return await CategoryDAO.Instance.GetCategoryCount();
        }

        public async Task<bool> UpdateCategoryAsync(int categoryId, string categoryName)
        {
            return await CategoryDAO.Instance.UpdateCategoryAsync(categoryId, categoryName);
        }

    }
}
