using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;

namespace Webstore_Admin.ViewModel
{
    public class DashboardViewModel
    {
        public List<KeyValuePair<Customer, decimal?>> TopCustomer { get; set; }
        public IEnumerable<Product> LowStockProducts { get; set; }
        public List<KeyValuePair<string, int?>> MostSoldProducts { get; set; }

        public decimal? TotalSalesMonth { get; set; }
    }
}
