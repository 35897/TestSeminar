using TestSeminar.Models.Binding;
using TestSeminar.Models.ViewModel;

namespace TestSeminar.Services.Interface
{
    public interface IProductService
    {
        Task<ProductViewModel> AddProductAsync(ProductBinding model);
        Task<ProductViewModel> GetProductAsync(int id);
        Task<List<ProductViewModel>> GetProductsAsync();
        Task<ProductViewModel> UpdateProductAsync(ProductUpdateBinding model);        
        Task DeleteProduct(int id);
        Task<ProductCategoryViewModel> AddProductCategoryAsync(ProductCategoryBinding model);
        Task<ProductCategoryViewModel> GetProductCategoryAsync(int id);
        Task<List<ProductCategoryViewModel>> GetProductCategoriesAsync();

        Task<List<ProductViewModel>> GetProductsFromCategoryAsync(int id);

        Task<ProductCategoryViewModel> UpdateProductCategoryAsync(ProductCategoryUpdateBinding model);
        Task DeleteProductCategory(int id);
    }
}