using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;
using Webstore_Admin.Models.Contracts;
using Webstore_Admin.Models.Helpers;

namespace Webstore_Admin.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(await PaginatedList<Discount>.CreateAsync(_discountRepository.GetAll, pageNumber, pageSize, ""));
        }

        public async Task<IActionResult> Specific(string name, int pageNumber = 1, int pageSize = 10)
        {
            return View(await PaginatedList<Discount>.CreateAsync(_discountRepository.GetSpecific(name), pageNumber, pageSize, ""));
        }

        public async Task<IActionResult> Create(Discount discount, bool copy = false)
        {
            if (ModelState.IsValid && copy != false)
            {
                await _discountRepository.AddAsync(discount);
                return RedirectToAction("Index");
            }
            return View(discount);
        }


        public async Task<IActionResult> Edit(int id, Discount discount)
        {
            var result = await _discountRepository.GetSingleAsync(id);

            if (ModelState.IsValid)
            {
                await _discountRepository.UpdateAsync(discount);
                return RedirectToAction("Index");
            }
            return View(result);
        }
    }
}
