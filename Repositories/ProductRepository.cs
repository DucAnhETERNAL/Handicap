using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public async Task Add(Product product)
        {
            await ProductDAO.Instance.Add(product);
        }

        public async Task Delete(int id)
        {
            await ProductDAO.Instance.Delete(id);
        }

        public async Task<IEnumerable<Product>> GetProductAll()
        {
            return await ProductDAO.Instance.GetProductAll();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await ProductDAO.Instance.GetProductById(id);
        }

        public async Task<int> GetProductCount()
        {
            return await ProductDAO.Instance.GetProductCount();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
            return await ProductDAO.Instance.GetProductsByCategory(categoryId);
        }

        public async Task Update(Product product)
        {
            await ProductDAO.Instance.Update(product);
        }
    }
}
