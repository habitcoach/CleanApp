using CleanDomain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infra.Data.Context
{
    public class RestaurantDBContext:DbContext
    {
        public RestaurantDBContext(DbContextOptions options):base(options) { }
        
        public DbSet<Product> Products { get; set; }
    }
   
}
