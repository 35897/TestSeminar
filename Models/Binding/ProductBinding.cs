using System.ComponentModel.DataAnnotations;
using TestSeminar.Models.Base;

namespace TestSeminar.Models.Binding
{
    public class ProductBinding : ProductBase
    {
        [Display(Name = "Kategorija")]
        public int ProductCategoryId { get; set; }
        [Display(Name = "Slika")]
        public IFormFile ProductImg { get; set; }   
    }
}
