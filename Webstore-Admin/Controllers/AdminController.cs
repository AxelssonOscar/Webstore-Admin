using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models.Contracts;

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

        public IActionResult OrderList(int? customerId)
        {
            if (customerId == null)
                return View(_orderRepository.GetOrders);
            else
                return View(_orderRepository.GetOrders.Where(x => x.CustomerId == customerId));
        }
        public IActionResult OrderDetails(int id)
        {
            var order = _orderRepository.GetOrders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(order);
            }
        }
    }
}
