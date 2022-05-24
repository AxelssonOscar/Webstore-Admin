﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models.Contracts;
using Webstore_Admin.Data;
using Microsoft.AspNetCore.Authorization;

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


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> LowStock()
        {
            return View(await _dashboardRepository.LowStockAsync());
        }




    }
}
