using CleanDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDomain.Queries
{
    public class GetProductsQuery:ProductQuery<IEnumerable<Product>>
    {
        public string? filterOn;
        public string? filterQuery;

        public string? sortBy;
        public bool? isAscending;

        public int? pageNumber;
        public int? pageSize;

        public GetProductsQuery(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int? pageNumber,
           int? pageSize)
        {
            this.filterOn = filterOn;
            this.filterQuery = filterQuery;
            this.sortBy = sortBy;
            this.isAscending = isAscending;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }
    }
}
