using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Webstore_Admin.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
