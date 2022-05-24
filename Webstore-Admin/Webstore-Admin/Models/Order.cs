using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Webstore_Admin.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "Ordernummer")]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "Orderdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderCreated { get; set; }

        public string WeatherType { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }


        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
