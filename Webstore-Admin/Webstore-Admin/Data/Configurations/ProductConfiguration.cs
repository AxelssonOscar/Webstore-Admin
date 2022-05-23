using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstore_Admin.Models;

namespace Webstore_Admin.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Name = "Royal Gala", CategoryId = 1, Price = 4.95m, UnitsInStock = 100 },
                new Product { Id = 2, Name = "Pink Lady", CategoryId = 1, Price = 4.95m, UnitsInStock = 15 },
                new Product { Id = 3, Name = "Granny Smith", CategoryId = 1, Price = 3.95m, UnitsInStock = 100 },
                new Product { Id = 4, Name = "Red Delicious", CategoryId = 1, Price = 2.95m, UnitsInStock = 70 },



                new Product { Id = 5, Name = "Calippo Cola", CategoryId = 2, Price = 8.95m, UnitsInStock = 10 },
                new Product { Id = 6, Name = "Twister", CategoryId = 2, Price = 9.95m, UnitsInStock = 3 },
                new Product { Id = 7, Name = "Piggelin", CategoryId = 2, Price = 5.95m, UnitsInStock = 13 },
                new Product { Id = 8, Name = "Nogger", CategoryId = 2, Price = 12.95m, UnitsInStock = 10 },
                new Product { Id = 9, Name = "88", CategoryId = 2, Price = 12.95m, UnitsInStock = 5 },
                new Product { Id = 10, Name = "Sandwich", CategoryId = 2, Price = 12.95m, UnitsInStock = 12 },
                new Product { Id = 11, Name = "Cornetto Jordgubb", CategoryId = 2, Price = 13.95m, UnitsInStock = 10 },
                new Product { Id = 12, Name = "Daim", CategoryId = 2, Price = 18.95m, UnitsInStock = 18 },
                new Product { Id = 13, Name = "Magnum Strawberry White", CategoryId = 2, Price = 18.95m, UnitsInStock = 18 },
                new Product { Id = 14, Name = "Lakrits Puck", CategoryId = 2, Price = 12.95m, UnitsInStock = 19 },


                new Product { Id = 15, Name = "Apple Juice", CategoryId = 3, Price = 16.95m, UnitsInStock = 20 },
                new Product { Id = 16, Name = "Apple Cider", CategoryId = 3, Price = 18.95m, UnitsInStock = 35 },
                new Product { Id = 17, Name = "Sweet Soda", CategoryId = 3, Price = 13.95m, UnitsInStock = 5 });

        }
    }
}
