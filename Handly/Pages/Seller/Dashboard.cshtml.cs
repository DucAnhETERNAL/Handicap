using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace Handly.Pages.Seller
{
    public class DashboardModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICategoryRepository _categoryRepository;
        
        public int TotalProducts { get; set; }
        


        public DashboardModel(IProductRepository productRepository, IUserRepository userRepository, IOrderItemRepository orderItemRepository, OrderRepository orderRepository, CategoryRepository categoryRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;

        }

        public async Task OnGetAsync()
        {
            TotalProducts = await _productRepository.GetProductCount();

        }
    }
}
