using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Data.Contexts;
using Webstore_Admin.Models;

namespace Webstore_Admin.Data.SeedData
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                SeedCustomer(context);
                SeedOrders(context);
            }
        }

        private static void SeedCustomer(AppDbContext context)
        {
            if (context.Customers.Any())
                return;


            CustomerCreator customerList = new CustomerCreator();
            customerList.LoadData();

            context.Customers.AddRange(customerList.customerList);

            context.SaveChanges();
            return;
        }

        private static void SeedOrders(AppDbContext context)
        {
            //if (context.Orders.Any())
                //return;

            OrderCreator orderList = new OrderCreator();
            orderList.Init(context);
            orderList.CreateOrders(context);

            //context.Orders.AddRange(orderList.orderList);

            //context.SaveChanges();
        }

    }
}
