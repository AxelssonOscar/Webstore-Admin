using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webstore_Admin.Models;

namespace Webstore_Admin.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Pippi Långstrump",
                    Address = "Kneippbyn 15",
                    City = "Visby",
                    ZipCode = "622 61",
                    PhoneNumber = "0701112233",
                    Email = "pippi@langstrump.se"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Karlsson på taket",
                    Address = "Rörstrandsgatan 25",
                    City = "Stockholm",
                    ZipCode = "113 41",
                    PhoneNumber = "0704445566",
                    Email = "karlsson@taket.se"
                },
                new Customer
                {
                    Id = 3,
                    Name = "Emil Svensson",
                    Address = "Katthult",
                    City = "Katthult",
                    ZipCode = "598 92",
                    PhoneNumber = "0707778899",
                    Email = "emil@svensson.se"
                },
                new Customer
                {
                    Id = 4,
                    Name = "Ronja Rövardotter",
                    Address = "Stränge 16",
                    ZipCode = "464 82",
                    City = "Mellerud",
                    PhoneNumber = "0701234567",
                    Email = "ronja@rovardotter.se"
                }
                );
        }
    }
}
