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
    public class UpdateProductCommandHandler:IRequestHandler<UpdateProductCommand,bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new Product()
            {
                Id = request.Id,
                Name = request.Name,
                ImageUrl = request.ImageUrl,
                Description = request.Description,
                Price = request.Price,

            };
            _productRepository.UpdateProduct(product);

            return Task.FromResult(true);
        }
    }
}
