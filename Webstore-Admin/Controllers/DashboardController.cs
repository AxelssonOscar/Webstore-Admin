using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models.Contracts;
using Webstore_Admin.Data;
using Microsoft.AspNetCore.Authorization;
using Webstore_Admin.ViewModel;

namespace Webstore_Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }


        public async Task<IActionResult> Index()
        {
            DashboardViewModel viewModel = new DashboardViewModel();
            viewModel.TopCustomer = await _dashboardRepository.TopCustomer();
            viewModel.LowStockProducts = await _dashboardRepository.LowStockAsync();
            return View(viewModel);
        }




    }
}