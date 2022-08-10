using TestSeminar.Models.Base;

namespace TestSeminar.Models.Binding
{
    public class ProductBinding : ProductBase
    {
        public int ProductCategoryId { get; set; }
        public IFormFile ProductImg { get; set; }   
    }
}
