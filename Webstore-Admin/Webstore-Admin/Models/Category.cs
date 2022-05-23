using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore_Admin.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Du måste ange namn...")]
        [Display(Name = "Kategorinamn")]
        public string Name { get; set; }
    }
}
