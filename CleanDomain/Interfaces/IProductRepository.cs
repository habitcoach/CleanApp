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
        IEnumerable<Product> GetProduct();
    }
}
