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
                new Product
                {
                    Id = 1,
                    Name = "Royal Gala",
                    CategoryId = 1,
                    Price = 4.95m,
                    UnitsInStock = 100
                },
                new Product
                {
                    Id = 2,
                    Name = "Pink Lady",
                    CategoryId = 1,
                    Price = 3.95m,
                    UnitsInStock = 100
                },

                new Product
                {
                    Id = 3,
                    Name = "Granny Smith",
                    CategoryId = 1,
                    Price = 5.95m,
                    UnitsInStock = 100
                },
                new Product
                {
                    Id = 4,
                    Name = "Red Delicious",
                    CategoryId = 1,
                    Price = 2.95m,
                    UnitsInStock = 100
                }
                );
        }
    }
}
