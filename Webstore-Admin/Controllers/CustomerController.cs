using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;
using Webstore_Admin.Models.Contracts;
using Webstore_Admin.Models.Helpers;
using Webstore_Admin.ViewModel;

namespace Webstore_Admin.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IActionResult> Index(int? customerId, string sortOrder, string name, string city, string address, int pageNumber = 1, int pageSize = 10)
        {
            CustomerSearchViewModel viewModel = new CustomerSearchViewModel();

            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(address))
            {
                var result = _customerRepository.Search(name, city, address);
                viewModel.Address = address;
                viewModel.City = city;
                viewModel.Name = name;

                switch (sortOrder)
                {
                    case "name_asc":
                        result = result.OrderBy(c => c.Name);
                        break;

                    case "city_asc":
                        result = result.OrderBy(c => c.City);
                        break;


                    case "address_asc":
                        result = result.OrderBy(c => c.Address);
                        break;

                    default:
                        result = result.OrderBy(c => c.Id);
                        break;
                }

                viewModel.customers = await PaginatedList<Customer>.CreateAsync(result, pageNumber, pageSize, sortOrder);
            }

            if (customerId != null)
            {
                viewModel.customers = await PaginatedList<Customer>.CreateAsync(_customerRepository.GetSingle(customerId), pageNumber, pageSize, sortOrder);
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