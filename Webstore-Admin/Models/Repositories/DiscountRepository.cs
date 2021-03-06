using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Data;
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
            _context.Discounts.Select(d => d).Include(d => d);

        public async Task<Tuple<Discount, List<Product>>> GetSpecific(int discountId)
        {
            List<Product> products = new List<Product>();

            var discountproducts = await _context.DiscountProducts.Where(dp => dp.DiscountId == discountId).Include(dp => dp.Product).ToListAsync();

            foreach (DiscountProduct dp in discountproducts)
            {
                products.Add(dp.Product);
            }

            var discount = await _context.Discounts.Where(d => d.Id == discountId).FirstOrDefaultAsync();

            var result = Tuple.Create(discount, products);

            return result;
        }

        public async Task<Discount> GetSingleAsync(int id) =>
            await _context.Discounts.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Discount> AddAsync(Discount discount, IEnumerable<int> selectedIds)
        {
            //discount.Product = _context.Products.Where(p => p.Id == discount.ProductId).FirstOrDefault();

            await _context.Discounts.AddAsync(discount);

            var products = _context.Products;

            foreach (int id in selectedIds)
            {
                DiscountProduct discountProduct = new DiscountProduct();

                discountProduct.Discount = discount;

                discountProduct.Product = products.Where(p => p.Id == id).FirstOrDefault();

                await _context.DiscountProducts.AddAsync(discountProduct);
            }

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
                //result.ProductId = discount.ProductId;
                //result.Product = discount.Product;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }


    }
}