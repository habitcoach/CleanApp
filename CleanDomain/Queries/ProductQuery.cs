using Clean.Domain.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDomain.Queries
{
    public abstract class ProductQuery<T>: Query<T>
    {
        public int Id { get; set; }
    }
}
