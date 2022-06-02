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

        public async Task<List<KeyValuePair<Customer, decimal>>> TopCustomer()
        {

            Dictionary<Customer, decimal> customers = new Dictionary<Customer, decimal>();

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
                    decimal sum = 0;
                    foreach (OrderDetail orderDetail in order.OrderDetails)
                    {
                        sum += orderDetail.Price;
                    }
                    customers.Add(customer, sum);
                }
            }

            var sortedCustomers = from entry in customers orderby entry.Value descending select entry;

            var top5Customers = sortedCustomers.Select(c => c).Take(10).ToList();

            //Tuple<Customer, decimal> result = Tuple.Create(highestCustomer, highestSum);

            return top5Customers;
        }



    }
}