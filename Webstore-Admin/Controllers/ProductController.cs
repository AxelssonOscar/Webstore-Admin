﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Webstore_Admin.Models;
using Webstore_Admin.Models.Contracts;

namespace Webstore_Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productRepository.GetAllAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Price, UnitsInStock, CategoryId")]Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var result = await _productRepository.GetSingleAsync(id);
            if (result == null)
            {
                return View("NotFound");
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            var result = await _productRepository.GetSingleAsync(id);

            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(product);
                return RedirectToAction("Index");
            }
            return View(result);            
        }
    }
}
