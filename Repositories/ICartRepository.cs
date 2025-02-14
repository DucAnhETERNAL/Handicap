using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetUserCartAsync(int userId);
        Task AddToCartAsync(int userId, int productId, int quantity);
        Task RemoveFromCartAsync(int cartId);
        Task ClearCartAsync(int userId);
    }
}
