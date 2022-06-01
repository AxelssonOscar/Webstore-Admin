using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Webstore_Admin.Data.Contexts;
using Webstore_Admin.Models.Contracts;

namespace Webstore_Admin.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Order> GetOrders =>
            _context.Orders.Include(x => x.Customer).Include(x => x.OrderDetails).ThenInclude(x => x.Product);

        public IQueryable<Order> GetAll =>
            _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Product);

        public string GetDistance(string city)
        {
            const string warehouse = "Örnsköldsvik";

            string result;

            using (WebClient wc = new WebClient())
            {
                var apiResult = wc.DownloadString("https://www.distance24.org/route.json?stops=" + city + "|" + warehouse);
                JObject json = JObject.Parse(apiResult);

                var distance = json.SelectToken("distance");

                string actualDistance = "";

                if (Int32.TryParse(distance.ToString(), out int parsedDistance))
                {
                    actualDistance = parsedDistance < 1500 ? parsedDistance.ToString() + "km" : "Did not find customer location";
                }

                result = actualDistance;
            }

            return result;
        }
    }
}
