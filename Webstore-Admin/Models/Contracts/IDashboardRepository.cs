using System;
using System.Collections.Generic;
using Webstore_Admin.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore_Admin.Models.Contracts
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<Product>> LowStockAsync();
        Task<List<KeyValuePair<Customer, decimal?>>> TopCustomer();

        Task<List<KeyValuePair<string, int?>>> MostSoldProducts();

        Task<decimal?> TotalSalesMonth();

    }
}
