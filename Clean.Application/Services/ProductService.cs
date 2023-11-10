using Clean.Application.DTOs;
using Clean.Application.Interfaces;
using Clean.Domain.Core.Bus;
using CleanDomain.Commands;
using CleanDomain.Interfaces;
using CleanDomain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IMediatorHandler _bus;

        public ProductService(IProductRepository ProductRepository, IMediatorHandler bus)
        {
            _ProductRepository = ProductRepository;
            _bus = bus;
        }

        public async Task<Dto> GetProduct()
        {
            return new Dto
            {

                Products = await _bus.SendCommandOrQuery(new GetProductsQuery())
            };
        }

        public async Task<ProductDto> GetProductById(ProductDto productdto)
        {
            var getProductById = new GetProductByIdQuery(
                productdto.Id
               );
            var product = await _bus.SendCommandOrQuery(getProductById);


            ProductDto dtoResult = new ProductDto() { Id = product.Id,
                                                   Name = product.Name,
                                                   Price = product.Price,
                                                   Description= product.Description,
                                                   ImageUrl = product.ImageUrl };

            return dtoResult;
        }

        public void Create(ProductDto productDto)
        {
            var createProductCommand = new CreateProductCommand(

                productDto.Name,
                productDto.Description,
                productDto.Price,
                productDto.ImageUrl

                );

            _bus.SendCommandOrQuery(createProductCommand);
        }

    }
}
