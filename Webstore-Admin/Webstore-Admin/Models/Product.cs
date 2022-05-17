using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webstore_Admin.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Du måste ange namn...")]
        [Display(Name = "Produkt Namn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Du måste ange pris...")]
        [Display(Name = "Pris")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Du måste ange antal i lager...")]
        [Display(Name = "Antal i lager")]
        public int UnitsInStock { get; set; }

        public ICollection<Discount> Discounts { get; set; }

      
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
