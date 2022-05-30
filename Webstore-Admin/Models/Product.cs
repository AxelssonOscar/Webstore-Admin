using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webstore_Admin.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name...")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter price...")]
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Please enter units in stock...")]
        [Display(Name = "In Stock")]
        public int? UnitsInStock { get; set; }

        [Required(ErrorMessage = "Please enter category...")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Discount> Discounts { get; set; }
    }
}
