using System.ComponentModel.DataAnnotations;
using TestSeminar.Models.Base;
using TestSeminar.Models.ViewModel;

namespace TestSeminar.Models.Binding
{
    public class ProductUpdateBinding : ProductBase
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategoryViewModel ProductCategory { get; set; }
        [Display(Name ="Slika")]
        public IFormFile ProductImg { get; set; }
    }
}
