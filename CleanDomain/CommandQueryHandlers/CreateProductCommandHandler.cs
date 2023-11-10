using CleanDomain.Commands;
using CleanDomain.Interfaces;
using CleanDomain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDomain.CommandQueryHandlers
{
    public class CreateProductCommandHandler:IRequestHandler<CreateProductCommand,bool>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new Product()
            {
                Name = request.Name,
                ImageUrl = request.ImageUrl,
                Description = request.Description,
                Price = request.Price
            };
            _productRepository.AddProduct(product);
            return Task.FromResult(true);
        
        }
    }
}
