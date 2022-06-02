using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore_Admin.Models
{
    public class DiscountProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}