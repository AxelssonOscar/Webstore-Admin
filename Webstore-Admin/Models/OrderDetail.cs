using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webstore_Admin.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Amount")]
        public int? Amount { get; set; }

        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
