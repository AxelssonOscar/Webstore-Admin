using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;

namespace Webstore_Admin.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Fruit" },
                new Category { Id = 2, Name = "GB" },
                new Category { Id = 3, Name = "Other" }
                );
        }
    }
}
