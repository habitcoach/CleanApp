﻿using Clean.Application.DTOs;
using Clean.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]


    //CRUD using CORS
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
     //   [Authorize(Roles = "Reader")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetProductsV1([FromQuery] string? filterOn, [FromQuery] string? filterQuery,

            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int? pageNumber =1, [FromQuery] int? pageSize=1000)
        {

            Dto products = await _productService.GetProduct(filterOn, filterQuery, sortBy, isAscending,
                pageNumber, pageSize);


            return Ok(products);

        }

        [HttpGet]
      //  [Authorize(Roles = "Reader")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetProductsV2([FromQuery] string? filterOn, [FromQuery] string? filterQuery,

           [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {

            Dto products = await _productService.GetProduct(filterOn, filterQuery, sortBy);


            return Ok(products);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Reader")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetProductsById(int id)
        {

            ProductDto product = await _productService.GetProductById(new ProductDto() { Id = id });


            return Ok(product);

        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ApiVersion("1.0")]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {

            _productService.Create(productDto);

            return Ok(productDto);

        }


        [HttpPut]
        [Authorize(Roles = "Writer")]
        [ApiVersion("1.0")]
        public IActionResult Update([FromBody] ProductDto productDto)
        {

             _productService.UpdateProduct(productDto);

            return Ok(productDto);

        }
    }
}
