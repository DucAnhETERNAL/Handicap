using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductAll();
        Task<Product?> GetProductById(int id);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(int id);
        Task<IEnumerable<Product>> GetProductsByCategory(int categoryId);
        Task<int> GetProductCount();
    }
}
