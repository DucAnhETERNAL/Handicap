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
        Task<List<Cart>> GetSelectedItemsAsync(List<int> selectedItemIds);
        Task DecreaseQuantityAsync(int userId, int productId); 
        Task ClearCartAsync(int userId);
    }
}
