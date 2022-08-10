using System.ComponentModel.DataAnnotations;

namespace TestSeminar.Models.Base
{
    public class ProductCategoryBase
    {
        [Display(Name = "Naziv")]
        public string Title { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}
