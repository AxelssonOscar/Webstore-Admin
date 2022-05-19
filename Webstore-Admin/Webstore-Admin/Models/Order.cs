using System;
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

        public DateTime OrderCreated { get; set; } = DateTime.Now;

        public string WeatherType { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }


        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
