using System.ComponentModel.DataAnnotations;

namespace TestSeminar.Models.Base
{
    public class ProductBase
    {
        [Display(Name = "Naziv")]
        public string Title { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Količina")]
        public decimal Quantity { get; set; }
        [Display(Name = "Cijena")]
        public decimal Price { get; set; }
        [Display(Name = "Slika")]
        public string? ProductImgUrl { get; set; }
    }
}
