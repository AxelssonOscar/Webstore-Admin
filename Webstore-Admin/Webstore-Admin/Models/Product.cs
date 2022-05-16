using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webstore_Admin.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Produkt Namn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Pris")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Antal i lager")]
        public int UnitsInStock { get; set; }

        public ICollection<Discount> Discounts { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
