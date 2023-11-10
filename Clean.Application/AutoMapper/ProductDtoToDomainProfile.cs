using AutoMapper;
using Clean.Application.DTOs;
using CleanDomain.Commands;
using CleanDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.AutoMapper
{
    public class ProductDtoToDomainProfile:Profile
    {
        public ProductDtoToDomainProfile()
        {
            CreateMap<ProductDto, CreateProductCommand>().
            ConstructUsing(c => new CreateProductCommand(c.Name, c.Description, c.Price, c.ImageUrl));

            CreateMap<Product, ProductDto>();

            CreateMap<ProductDto, UpdateProductCommand>();

        }
    }
}
