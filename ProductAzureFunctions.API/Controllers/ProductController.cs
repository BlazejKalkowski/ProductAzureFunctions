using Microsoft.AspNetCore.Mvc;
using ProductAzureFunctions.API.Models;
using ProductAzureFunctions.API.Services;

namespace ProductAzureFunctions.API.Controllers
{
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService, IConfiguration configuration)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO dto)
        {
            var result = await _productService.CreateProduct(dto);
            return Ok(result);
        }
    }
}
