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
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;
        const int LOW_STOCK_VALUE = 10;
        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> LowStockAsync() =>
            await _context.Products.Where(p => p.UnitsInStock < LOW_STOCK_VALUE).ToListAsync();

        public async Task<List<KeyValuePair<Customer, decimal?>>> TopCustomer()
        {

            Dictionary<Customer, decimal?> customers = new Dictionary<Customer, decimal?>();

            var orders = await _context.Orders.Where(o => o.OrderCreated > DateTime.Now.AddMonths(-1)).Include(o => o.Customer).Include(o => o.OrderDetails).ThenInclude(o => o.Product).ToListAsync();


            foreach (Order order in orders)
            {
                if (customers.ContainsKey(order.Customer))
                {
                    foreach (OrderDetail orderDetail in order.OrderDetails)
                    {
                        customers[order.Customer] += orderDetail.Price;
                    }
                }
                else
                {
                    Customer customer = order.Customer;
                    decimal? sum = 0;
                    foreach (OrderDetail orderDetail in order.OrderDetails)
                    {
                        sum += orderDetail.Price;
                    }
                    customers.Add(customer, sum);
                }
            }

            var sortedCustomers = from entry in customers orderby entry.Value descending select entry;

            var top5Customers = sortedCustomers.Select(c => c).Take(5).ToList();

            //Tuple<Customer, decimal> result = Tuple.Create(highestCustomer, highestSum);

            return top5Customers;
        }

        public async Task<List<KeyValuePair<string, int?>>> MostSoldProducts()
        {

            Dictionary<string, int?> products = new Dictionary<string, int?>();            

            var orders = await _context.Orders.Where(o => o.OrderCreated > DateTime.Now.AddMonths(-1)).ToListAsync();


            foreach (Order order in orders)
            {
                for (int i = 0; i < order.OrderDetails.Count; i++)
                {
                    var productName = order.OrderDetails.Select(p => p.Product.Name).ElementAtOrDefault(i);
                    var amount = order.OrderDetails.Select(a => a.Amount).ElementAtOrDefault(i);
                    if (!products.ContainsKey(productName))
                    {
                        products.Add(productName, amount);

                    }
                    else
                    {
                        products[productName] += amount;
                    }
                }

            }

            var sortedProducts = from entry in products orderby entry.Value descending select entry;

            var top5SoldProducts = sortedProducts.Select(p => p).Take(5).ToList();

            return top5SoldProducts;
        }

        public async Task<List<decimal?>> TotalSalesMonth()
        {
            List<decimal?> totalSum = new List<decimal?>();
            
            var orders = await _context.Orders.Where(o => o.OrderCreated > DateTime.Now.AddMonths(-1)).ToListAsync();

            decimal? sum = 0;

            foreach (Order order in orders)
            {
                
                foreach (OrderDetail orderDetail in order.OrderDetails)
                {
                    sum += orderDetail.Price;
                }
                
            }

            totalSum.Add(sum);

            return totalSum;
        }
    }
}