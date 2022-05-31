using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webstore_Admin.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Discount name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Rabatt")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Percent { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
