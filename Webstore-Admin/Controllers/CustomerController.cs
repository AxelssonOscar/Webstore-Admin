using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models.Contracts;
using Webstore_Admin.ViewModel;

namespace Webstore_Admin.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IActionResult> Index(string name, string city, string address, int customerId)
        {
            CustomerSearchViewModel viewModel = new CustomerSearchViewModel();

            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(address))
            {
                viewModel.customers = await _customerRepository.Search(name, city, address);
                viewModel.sum = viewModel.customers.Count();
            }

            if (customerId > 0)
            {
                viewModel.customers = await _customerRepository.GetSingle(customerId);
                viewModel.sum = 1;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int customerId)
        {
            CustomerDetailsViewModel viewModel = new CustomerDetailsViewModel();

            var result = await _customerRepository.GetSingleAndDistanceAsync(customerId);
            viewModel.customer = result.Item1;
            viewModel.distance = result.Item2;
            return View(viewModel);
        }


    }
}