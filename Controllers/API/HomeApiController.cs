using Microsoft.AspNetCore.Mvc;
using TestSeminar.Services.Interface;

namespace TestSeminar.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly IProductService productService;

        public HomeApiController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [Route("proizvodi")]
        public async Task<IActionResult> Products()
        {
            return Ok(await productService.GetProductsAsync());
        }

        [HttpGet]
        [Route("proizvodi/{id}")]
        public async Task<IActionResult> Product(int id)
        {
            return Ok(await productService.GetProductAsync(id));
        }
    }
}
