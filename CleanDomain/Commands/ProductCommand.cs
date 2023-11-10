using Clean.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDomain.Commands
{
    public class ProductCommand:Command
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
