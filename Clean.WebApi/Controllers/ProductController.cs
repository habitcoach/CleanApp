using Clean.Application.DTOs;
using Clean.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {

            Dto products = await _productService.GetProduct();


            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById(int id)
        {

            ProductDto product = await _productService.GetProductById(new ProductDto() { Id = id });


            return Ok(product);

        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {

            _productService.Create(productDto);

            return Ok(productDto);

        }
    }
}
