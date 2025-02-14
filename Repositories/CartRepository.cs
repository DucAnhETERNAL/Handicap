using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CartRepository : ICartRepository
    {
        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            await CartDAO.Instance.AddToCartAsync(userId, productId, quantity);
        }

        public async Task ClearCartAsync(int userId)
        {
            await CartDAO.Instance.ClearCartAsync(userId);
        }

        public async Task<List<Cart>> GetUserCartAsync(int userId)
        {
            return await CartDAO.Instance.GetUserCartAsync(userId);
        }

        public async Task RemoveFromCartAsync(int cartId)
        {
            await CartDAO.Instance.RemoveFromCartAsync(cartId);
        }
    }
}
