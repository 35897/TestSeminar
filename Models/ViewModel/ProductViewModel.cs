using System.ComponentModel.DataAnnotations;
using TestSeminar.Models.Base;

namespace TestSeminar.Models.ViewModel
{
    public class ProductViewModel : ProductBase
    {
        public int Id { get; set; }
        [Display(Name = "Kategorija")]
        public ProductCategoryViewModel ProductCategory { get; set; }
    }
}
