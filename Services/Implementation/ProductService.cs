using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestSeminar.Data;
using TestSeminar.Models.Binding;
using TestSeminar.Models.Dbo;
using TestSeminar.Models.ViewModel;
using TestSeminar.Services.Interface;

namespace TestSeminar.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        public ProductService(ApplicationDbContext db, IMapper mapper, IFileStorageService fileStorageService)
        {
            this.db = db;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }

        //dodavanje proizvoda
        public async Task<ProductViewModel> AddProductAsync(ProductBinding model)
        {
            var dbo = mapper.Map<Product>(model);
            var file = await fileStorageService.AddFileToStorage(model.ProductImg);
            if (file != null)
            {
                dbo.ProductImgUrl = file.DownloadUrl;
            }
            var productCategory = await db.ProductCategory.FindAsync(model.ProductCategoryId);
            if (productCategory == null)
            {
                return null;
            }
            //kategoriju isto treba doradit
            dbo.ProductCategory = productCategory;
            db.Product.Add(dbo);
            await db.SaveChangesAsync();    
            return mapper.Map<ProductViewModel>(dbo);
        }

        // dohvat proizvoda po id
        public async Task<ProductViewModel> GetProductAsync(int id)
        {
            var dbo = await db.Product
                .Include(x => x.ProductCategory)
                .FirstOrDefaultAsync(x=>x.Id == id);
            //dodat za kategoriju 
            return mapper.Map<ProductViewModel>(dbo);
        }

        // dohvat svih proizvoda
        public async Task<List<ProductViewModel>> GetProductsAsync()
        {
            var dbo = await db.Product
                .Include(x=>x.ProductCategory)
                .ToListAsync();
            return dbo.Select(x => mapper.Map<ProductViewModel>(x)).ToList();
        }

        //update proizvoda
        public async Task<ProductViewModel> UpdateProductAsync(ProductUpdateBinding model)
        {
            //dodat kategoriju
            var category = await db.ProductCategory.FirstOrDefaultAsync(x => x.Id == model.ProductCategoryId);
            var oldImage = db.Product.FirstOrDefaultAsync(x => x.Id == model.Id).Result.ProductImgUrl;
           
            var dbo = await db.Product.FindAsync(model.Id);
            var file = await fileStorageService.AddFileToStorage(model.ProductImg);
            mapper.Map(model, dbo);   
            if(model.ProductImg == null)
            {
                dbo.ProductImgUrl = oldImage;
            }
            else
            {
                dbo.ProductImgUrl = file.DownloadUrl;
            }
            dbo.ProductCategory = category;            
            await db.SaveChangesAsync();
            return mapper.Map<ProductViewModel>(dbo);
        }
        //brisanje proizvoda
        public async Task DeleteProduct(int id)
        {
            var dbo = await db.Product.FindAsync(id);
            if (dbo != null) 
            {
               db.Remove(dbo);
            }
            await db.SaveChangesAsync();            
        }     

        //dodavanje kategorije proizvoda
        public async Task<ProductCategoryViewModel> AddProductCategoryAsync(ProductCategoryBinding model)
        {
            var dbo = mapper.Map<ProductCategory>(model);
            db.ProductCategory.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);
        }

        // dohvat kategorije proizvoda po id
        public async Task<ProductCategoryViewModel> GetProductCategoryAsync(int id)
        {
            var dbo = await db.ProductCategory.FindAsync(id);            
            return mapper.Map<ProductCategoryViewModel>(dbo);
        }

        // dohvat svih kategorija proizvoda
        public async Task<List<ProductCategoryViewModel>> GetProductCategoriesAsync()   
        {
            var dbo = await db.ProductCategory.ToListAsync();
            return dbo.Select(x => mapper.Map<ProductCategoryViewModel>(x)).ToList();
        }


        // dohvat svih proizvoda iz kategorije
        public async Task<List<ProductViewModel>> GetProductsFromCategoryAsync(int id)
        {
            var dbo = await db.Product
                .Include(x => x.ProductCategory)
                .Where(y=>y.ProductCategory.Id == id)
                .ToListAsync();
            return dbo.Select(x => mapper.Map<ProductViewModel>(x)).ToList();
        }

        //update kategorije proizvoda
        public async Task<ProductCategoryViewModel> UpdateProductCategoryAsync(ProductCategoryUpdateBinding model)
        {
            
            var dbo = await db.ProductCategory.FindAsync(model.Id);            
            mapper.Map(model, dbo);
          
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);
        }
        //brisanje kategorije proizvoda
        public async Task DeleteProductCategory(int id)
        {
            var dbo = await db.ProductCategory.FindAsync(id);
            //var products = await db.Product
            //    .Include(x => x.ProductCategory)
            //    .Where(y => y.ProductCategory.Id == id)
            //    .ToListAsync();
            //foreach (var item in products)
            //{
            //    item.ProductCategory = new ProductCategory();
            //}

            if (dbo != null)
            {
                db.Remove(dbo);
            }
            await db.SaveChangesAsync();
        }

    }
}
