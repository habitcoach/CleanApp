using Clean.Application.DTOs;
using CleanDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _ProductRepository;

        public ProductService(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }

        public Dto GetProduct()
        {
            return new Dto
            {

                Products = _ProductRepository.GetProduct()
            };
        }
    }
}
