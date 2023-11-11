using Clean.Application.DTOs;
using Clean.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]

    //CRUD using CORS
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] string? filterOn, [FromQuery] string? filterQuery,

            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int? pageNumber =1, [FromQuery] int? pageSize=1000)
        {

            Dto products = await _productService.GetProduct(filterOn, filterQuery, sortBy, isAscending,
                pageNumber, pageSize);


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


        [HttpPut]
        public IActionResult Update([FromBody] ProductDto productDto)
        {

             _productService.UpdateProduct(productDto);

            return Ok(productDto);

        }
    }
}
