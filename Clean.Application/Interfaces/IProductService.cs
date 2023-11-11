using Clean.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.Interfaces
{
    public interface IProductService
    {
        public Task<Dto> GetProduct(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true,
            int? pageNumber = 1, int? pageSize = 10);
        public Task<ProductDto> GetProductById(ProductDto productdto);

        public void Create(ProductDto productDto);

        public void UpdateProduct(ProductDto productDto);
    }
}
