using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;

namespace Webstore_Admin.ViewModel
{
    public class DiscountCreateViewModel
    {
        public Discount Discount { get; set; }
        public ICollection<DiscountProduct> DiscountProducts { get; set; }

        public IEnumerable<int> SelectedProductIds { get; set; }
    }
}