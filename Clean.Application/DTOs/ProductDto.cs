using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
