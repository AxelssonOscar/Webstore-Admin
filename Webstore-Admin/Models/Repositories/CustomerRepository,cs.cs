using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Data.Contexts;
using Webstore_Admin.Models.Contracts;

namespace Webstore_Admin.Models.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> Search(string name, string city, string address)
        {
            List<Customer> customers = await _context.Customers.Where(c => c.Address.Contains(address)
                                                                        || c.City.Contains(city)
                                                                        || c.Name.Contains(name))
                                                                        .Include(c => c).ToListAsync();

            return customers;
        }

        public async Task<IEnumerable<Customer>> GetSingle(int id) =>
            await _context.Customers.Where(c => c.Id == id).ToListAsync();




        //public async Task<Tuple<Customer, string>> GetOrdersById(int id)
        //{
        //    const string warehouse = "Örnsköldsvik";

        //    var customer = await _context.Customers.Where(c => c.Id == id).Include(c => c.Orders).ThenInclude(o => o.OrderDetails).ThenInclude(o => o.Product).SingleAsync();

        //    Tuple<Customer, string> result;

        //    using (WebClient wc = new WebClient())
        //    {
        //        var apiResult = wc.DownloadString("https://www.distance24.org/route.json?stops=" + customer.City + "|" + warehouse);
        //        JObject json = JObject.Parse(apiResult);

        //        var distance = json.SelectToken("distance");
        //        result = Tuple.Create(customer, distance.ToString());
        //    }

        //    return result;
        //}
    }
}