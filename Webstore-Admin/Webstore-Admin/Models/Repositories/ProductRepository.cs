using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Webstore_Admin.Data.Contexts;
using Webstore_Admin.Models.Contracts;

namespace Webstore_Admin.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product)
        {
            var entity = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Product> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product;
            }
            return null;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var result = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (result != null)
            {
                result.Name = product.Name;
                result.Price = product.Price;
                result.UnitsInStock = product.UnitsInStock;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products.ToListAsync();

        public async Task<Product> GetSingleAsync(int id) =>
            await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

    }
}
