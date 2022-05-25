using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;

namespace Webstore_Admin.ViewModel
{
    public class DashboardViewModel
    {
        public Tuple<Customer, decimal> TopCustomer { get; set; }
        public IEnumerable<Product> LowStockProducts { get; set; }
    }
}
