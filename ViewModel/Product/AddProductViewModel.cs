using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel
{
    public class AddProductViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please, Provide a valid Product Name")]
        [StringLength(50, ErrorMessage = "Must be more 5 letter", MinimumLength = 5)]
        public string Name { get; set; }
        [MultiLines]
        public string Description { get; set; }

        [Column(TypeName = "number")]
        [Required(ErrorMessage = "Please, Provide a valid Product Price")]
        public double Price { get; set; }

        [Column(TypeName = "number")]
        [Required(ErrorMessage = "Please, Provide a valid Product Qty")]
        public int Quantity { get; set; }
        [Display(Name = "Choose Product Category")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Please, Provide atleast one image")]
        public IFormFileCollection Images { get; set; }
        public List<string> ImagesURL { get; set; } = new List<string>();
        //
        public bool KeepImages { get; set; } = true;
    }
}
