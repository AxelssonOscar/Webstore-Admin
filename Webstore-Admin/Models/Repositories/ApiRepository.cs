using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Data.Contexts;
using Webstore_Admin.Models.Contracts;

namespace Webstore_Admin.Models.Repositories
{
    public class ApiRepository : IApiRepository
    {
        private AppDbContext _context;

        public ApiRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await _context.Products.ToListAsync();

        public async Task<Product> GetProductAsync(int id) =>
            await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
}
