using System.ComponentModel.DataAnnotations;

namespace Webstore_Admin.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter category name...")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
