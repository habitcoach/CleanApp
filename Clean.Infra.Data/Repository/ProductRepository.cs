using Clean.Infra.Data.Context;
using CleanDomain.Interfaces;
using CleanDomain.Models;
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
        public IEnumerable<Product> GetProduct()
        {
            return _context.Products;
        }
    }
}
