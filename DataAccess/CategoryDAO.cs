using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class CategoryDAO : SingletonBase<CategoryDAO>
    {
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task<bool> AddCategoryAsync(string categoryName)
        {
            if (await _context.Categories.AnyAsync(c => c.CategoryName == categoryName))
                return false; // Danh mục đã tồn tại

            var category = new Category { CategoryName = categoryName };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(int categoryId, string categoryName)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) return false;

            category.CategoryName = categoryName;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetCategoryCount()
        {
            return await _context.Categories.CountAsync();
        }
    }
}
