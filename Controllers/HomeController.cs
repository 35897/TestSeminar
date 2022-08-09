using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestSeminar.Models;
using TestSeminar.Services.Interface;

namespace TestSeminar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // servis ne zaboravi dodat u kontejner
        private readonly IProductService productService;


        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            //testni ispis svih
            return View(productService.GetProductsAsync().Result);
        }

        public IActionResult Privacy()
        {
            return View();
        }    
        public async Task<IActionResult> ProductDetails(int id)   
        {
            var product = await productService.GetProductAsync(id);
            return View(product);
        }
        public async Task<IActionResult> CategoryDetails(int id)
        {
            var productsInCategory = await productService.GetProductsFromCategoryAsync(id);
            return View(productsInCategory);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}