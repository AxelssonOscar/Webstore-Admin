using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        public DiscountController(IDiscountRepository discountRepository, IProductRepository productRepository)
        {
            _discountRepository = discountRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 100)
        {
            return View(await PaginatedList<Discount>.CreateAsync(_discountRepository.GetAll, pageNumber, pageSize, ""));
        }

        public async Task<IActionResult> DiscountDetails(int discountId)
        {
            return View(await _discountRepository.GetSpecific(discountId));
        }

        public async Task<IActionResult> Create(DiscountCreateViewModel viewModel, bool copy = false)
        {

            DiscountCreateViewModel _viewModel = new DiscountCreateViewModel();

            if (ModelState.IsValid && copy != false)
            {
                await _discountRepository.AddAsync(viewModel.Discount, viewModel.SelectedProductIds);
                return RedirectToAction("Index");
            }

            ViewBag.Products = new MultiSelectList(_productRepository.GetAll.OrderBy(p => p.Category), "Id", "Name");

            return View(viewModel);
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
