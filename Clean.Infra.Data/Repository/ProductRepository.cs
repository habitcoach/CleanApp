using Clean.Infra.Data.Context;
using CleanDomain.Interfaces;
using CleanDomain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infra.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly RestaurantDBContext _context;
        public ProductRepository(RestaurantDBContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public Task<List<Product>> GetProduct(string? filterOn, string? filterQuery,
           string? sortBy, bool? isAscending = true, int? PageNumber = 1, int? PageSize = 10)
        {
            var products = _context.Products.AsQueryable();
            //filter
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {

                if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                    products = products.Where(x => x.Description.Contains(filterQuery));

            }
            //sort
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {

                if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                    products = isAscending ?? true ? products.OrderBy(x => x.Price) : products.OrderByDescending(x => x.Price);

            }

            //Pagination

            var skipResult = (PageNumber - 1) * PageSize;

            return products.Skip((int)skipResult).Take((int)PageSize).ToListAsync();
        }
        async Task<Product> IProductRepository.GetProductById(int id)
        {
            var product = _context.Products.Find(id);
            return product;
        }

        public void UpdateProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
           
        }
    }
}
