using AutoMapper;
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
        private readonly IMapper _autoMapper;

        public ProductService(IProductRepository ProductRepository, IMediatorHandler bus, IMapper autoMapper)
        {
            _ProductRepository = ProductRepository;
            _bus = bus;
            _autoMapper = autoMapper;
        }

        public async Task<Dto> GetProduct(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending,
             int? pageNumber = 1, int? pageSize = 10)
        {
            return new Dto
            {

                Products = await _bus.SendCommandOrQuery(new GetProductsQuery(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize))
            };
        }

        public async Task<ProductDto> GetProductById(ProductDto productdto)
        {
            var getProductById = new GetProductByIdQuery(
                productdto.Id
               );
            var product = await _bus.SendCommandOrQuery(getProductById);

            #region automapper for productDto
            //ProductDto dtoResult = new ProductDto() { Id = product.Id,
            //                                       Name = product.Name,
            //                                       Price = product.Price,
            //                                       Description= product.Description,
            //                                       ImageUrl = product.ImageUrl };



            //return dtoResult;
            #endregion

            return _autoMapper.Map<ProductDto>(product);
        }

        public void Create(ProductDto productDto)
        {
            #region automapper for createProductCommand
            //var createProductCommand = new CreateProductCommand(

            //    productDto.Name,
            //    productDto.Description,
            //    productDto.Price,
            //    productDto.ImageUrl

            //    );
            #endregion

            _bus.SendCommandOrQuery(_autoMapper.Map<CreateProductCommand>(productDto));
        }


        public void UpdateProduct(ProductDto productDto)
        {
            #region automapper for updateProductCommand
            //var updateProductCommand = new UpdateProductCommand(
            //    productDto.Id,
            //    productDto.Name,
            //    productDto.Description,
            //    productDto.Price,
            //    productDto.ImageUrl

            //    );
            #endregion

            _bus.SendCommandOrQuery(_autoMapper.Map<UpdateProductCommand>(productDto));
            
           
        }

    }
}
