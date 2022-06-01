using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webstore_Admin.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Kampanj Namn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Rabatt")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Percent { get; set; }

        [Required]
        [Display(Name = "Start Datum")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Slut Datum")]
        public DateTime EndDate { get; set; }

        public ICollection<DiscountProduct> DiscountProducts { get; set; }
    }
}
