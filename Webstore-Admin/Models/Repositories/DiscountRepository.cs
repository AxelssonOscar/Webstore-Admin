using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Data.Contexts;
using Webstore_Admin.Models.Contracts;

namespace Webstore_Admin.Models.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly AppDbContext _context;
        public DiscountRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Discount> GetAll =>
            _context.Discounts.Select(d => d).Include(d => d.Product);

        public IQueryable<Discount> GetSpecific(string discountName) =>
            _context.Discounts.Where(d => d.Name == discountName || d.Product.Name == discountName).Select(d => d).Include(d => d.Product);

        public async Task<Discount> GetSingleAsync(int id) =>
            await _context.Discounts.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Discount> AddAsync(Discount discount)
        {
            discount.Product = _context.Products.Where(p => p.Id == discount.ProductId).FirstOrDefault();

            await _context.Discounts.AddAsync(discount);
            await _context.SaveChangesAsync();
            return discount;
        }        

        public async Task<Discount> UpdateAsync(Discount discount)
        {
            var result = await _context.Discounts.FirstOrDefaultAsync(p => p.Id == discount.Id);
            if (result != null)
            {
                result.Name = discount.Name;
                result.Percent = discount.Percent;
                result.StartDate = discount.StartDate;
                result.EndDate = discount.EndDate;
                result.ProductId = discount.ProductId;
                result.Product = discount.Product;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        
    }
}
