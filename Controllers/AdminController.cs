using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestSeminar.Models;
using TestSeminar.Models.Binding;
using TestSeminar.Services.Interface;

namespace TestSeminar.Controllers
{
    // ne zaboravi odkomentirat 
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        private readonly IUserService userService;


        public AdminController(IProductService productService, IMapper mapper, IUserService userService)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task<IActionResult> ProductAdministration()
        {
            var products = await productService.GetProductsAsync();

            return View(products);
        }
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductBinding model)
        {
            await productService.AddProductAsync(model);
            return RedirectToAction("ProductAdministration");
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await productService.GetProductAsync(id);
            var model = mapper.Map<ProductUpdateBinding>(product);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpdateBinding model)
        {
            var product = await productService.UpdateProductAsync(model);
            return RedirectToAction("ProductAdministration");
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productService.GetProductAsync(id);
            var model = mapper.Map<ProductUpdateBinding>(product);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductUpdateBinding model)
        {
            await productService.DeleteProduct(model.Id);
            return RedirectToAction("ProductAdministration");
        }

        public async Task<IActionResult> AddProductCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductCategory(ProductCategoryBinding model)
        {
            await productService.AddProductCategoryAsync(model);
            return RedirectToAction("ProductAdministration");
        }

        public async Task<IActionResult> UpdateProductCategory(int id)
        {
            var product = await productService.GetProductCategoryAsync(id);
            var model = mapper.Map<ProductCategoryUpdateBinding>(product);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductCategory(ProductCategoryUpdateBinding model)
        {
            var product = await productService.UpdateProductCategoryAsync(model);
            return RedirectToAction("ProductAdministration");
        }
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var product = await productService.GetProductCategoryAsync(id);
            var model = mapper.Map<ProductCategoryUpdateBinding>(product);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductCategory(ProductCategoryUpdateBinding model)
        {
            await productService.DeleteProductCategory(model.Id);
            return RedirectToAction("ProductAdministration");
        }
        public async Task<IActionResult> UserAdministration()
        {
            var users = await userService.GetUsersAsync();

            return View(users);
        }

        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = await userService.GetUserAsync(id);            
            return View(user); 
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(ApplicationUserUpdateBinding model)
        {
            var product = await userService.UpdateUserAsync(model);
            return RedirectToAction("UserAdministration");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userService.GetUserAsync(id);
            var model = mapper.Map<ApplicationUserUpdateBinding>(user);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(ApplicationUserUpdateBinding model)
        {
            await userService.DeleteUserAsync(model.Id);
            return RedirectToAction("UserAdministration");
        }

        public async Task<IActionResult> AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(ApplicationUserCreateBinding model)
        {
            await userService.AddUserAsync(model);
            return RedirectToAction("UserAdministration");
        }
    }
}
