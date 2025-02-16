using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace Handly.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        

        public int TotalUsers { get; set; }
        public int TotalProducts { get; set; }
        
        public  int TotalSeller {  get; set; }
        public int TotalCustomer { get; set; }
        public int TotalShipper { get; set; }

        public DashboardModel(IProductRepository productRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
           
        }

        public async Task OnGetAsync()
        {
            TotalUsers = await _userRepository.GetUserCount();
            TotalProducts = await _productRepository.GetProductCount();
            
            TotalCustomer = await _userRepository.GetCustomerCount();
            TotalSeller = await _userRepository.GetSellerCount();
            TotalShipper = await _userRepository.GetShipperCount();
        }
    }
}
