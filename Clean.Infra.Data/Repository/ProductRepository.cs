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

        public Task<List<Product>> GetProduct()
        {
            return _context.Products.ToListAsync();
        }
        async Task<Product> IProductRepository.GetProductById(int id)
        {
            var product = _context.Products.Find(id);
            return product;
        }
    }
}
