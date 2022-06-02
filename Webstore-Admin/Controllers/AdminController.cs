using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;
using Webstore_Admin.Models.Contracts;
using Webstore_Admin.Models.Helpers;
using Webstore_Admin.ViewModel;

namespace Webstore_Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public AdminController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> OrderList(int? customerId, int pageNumber = 1, int pageSize = 10)
        {
            if (customerId == null)
            {
                return View(await PaginatedList<Order>.CreateAsync(_orderRepository.GetAll, pageNumber, pageSize, ""));
            }
            else
            {
                return View(await PaginatedList<Order>.CreateAsync(_orderRepository.GetAll.Where(x => x.CustomerId == customerId), pageNumber, pageSize, ""));
            }
        }

        public IActionResult OrderDetails(int id)
        {
            var vm = new OrderDetailsViewModel();
            vm.Order = _orderRepository.GetSingle(id);
            vm.Distance = _orderRepository.GetDistance(vm.Order.Customer.City);

            if (vm == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(vm);
            }
        }
    }
}
