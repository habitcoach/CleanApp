using CleanDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDomain.Queries
{
    public class GetProductByIdQuery: ProductQuery<Product>
    {
        public GetProductByIdQuery(int id)
        {
                this.Id = id;
        }
    }
}
