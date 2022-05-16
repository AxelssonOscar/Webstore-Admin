using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Webstore_Admin.Models
{
    //Exempelkategori: Bok, Äpple, Tillbehör, Recept
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Kategori")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
