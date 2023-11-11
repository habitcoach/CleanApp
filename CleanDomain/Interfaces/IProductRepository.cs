using CleanDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDomain.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProduct(string? filterOn, string? filterQuery, string?
           sortBy = null, bool? isAscending = true, int? PageNumber = 1, int? PageSize = 10);
        public Task<Product> GetProductById(int id);

        public void AddProduct(Product product);

        public void UpdateProduct(Product product);

    }
}
