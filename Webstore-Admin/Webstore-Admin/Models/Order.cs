using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Webstore_Admin.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
