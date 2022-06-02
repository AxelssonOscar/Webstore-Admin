using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public IQueryable<Customer> Search(string name, string city, string address) =>
            _context.Customers.Where(c => c.Address.Contains(address)
                                        || c.City.Contains(city)
                                        || c.Name.Contains(name))
                                        .Include(c => c);

        public IQueryable<Customer> BadSearch(string name, string city, string address)
        {
            //Name exist, city does not exist, address does not exist
            if(name != "" && city ==  null && address == null)
            {
                return _context.Customers.Where(c => c.Name.Contains(name)).Include(c => c);
            }

            //Name does not exist, city exist, address does not exist
            if (name == null && city != "" && address == null)
            {
                return _context.Customers.Where(c => c.City.Contains(city)).Include(c => c);
            }

            //Name does not exist, city does not exist, address exist
            if (name == null && city == null && address != "")
            {
                return _context.Customers.Where(c => c.Address.Contains(address)).Include(c => c);
            }

            //Name exist, city exist, address does not exist
            if (name != "" && city != "" && address == null)
            {
                return _context.Customers.Where(c => c.Name.Contains(name)
                                                  && c.City.Contains(city)).Include(c => c);
            }

            //Name exist, city does not exist, address exist
            if (name != "" && city == null && address != "")
            {
                return _context.Customers.Where(c => c.Name.Contains(name)
                                                  && c.Address.Contains(address)).Include(c => c);
            }

            //Name does not exist, city exist, address exist
            if (name == null && city != "" && address != "")
            {
                return _context.Customers.Where(c => c.City.Contains(city)
                                                  && c.Address.Contains(address)).Include(c => c);
            }

            //If all fields exist, return this.
                return _context.Customers.Where(c => c.Name.Contains(name)
                                                  && c.City.Contains(city)
                                                  && c.Address.Contains(address)).Include(c => c);
            
        }


        public IQueryable<Customer> GetSingle(int? id) =>
            _context.Customers.Where(c => c.Id == id);




        public async Task<Tuple<Customer, string>> GetSingleAndDistanceAsync(int id)
        {
            const string warehouse = "Örnsköldsvik";

            var customer = await _context.Customers.Where(c => c.Id == id).Include(c => c.Orders).ThenInclude(o => o.OrderDetails).ThenInclude(o => o.Product).SingleAsync();

            Tuple<Customer, string> result;

            using (WebClient wc = new WebClient())
            {
                var apiResult = wc.DownloadString("https://www.distance24.org/route.json?stops=" + customer.City + "|" + warehouse);
                JObject json = JObject.Parse(apiResult);

                var distance = json.SelectToken("distance");

                string actualDistance = "";

                if (Int32.TryParse(distance.ToString(), out int parsedDistance))
                {
                    actualDistance = parsedDistance < 1500 ? parsedDistance.ToString() + "km" : "Did not find customer location";
                }

                result = Tuple.Create(customer, actualDistance);
            }

            return result;
        }
    }
}