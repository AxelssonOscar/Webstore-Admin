using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models.Contracts;

namespace Webstore_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : Controller
    {
        private readonly IApiRepository _apiRepository;

        public Products(IApiRepository ApiRepository)
        {
            _apiRepository = ApiRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _apiRepository.GetAllProductsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _apiRepository.GetProductAsync(id);

            if (product != null)
                return Ok(product);
            else
                return NotFound();
        }
    }
}
