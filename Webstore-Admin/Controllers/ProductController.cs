using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Webstore_Admin.Models;
using Webstore_Admin.Models.Contracts;

namespace Webstore_Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productRepository.GetAllAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetCategories, "Id", "Name");
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
            ViewBag.Categories = new SelectList(_categoryRepository.GetCategories, "Id", "Name");
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
