using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Range(1,double.MaxValue,ErrorMessage ="price cannot be negative")]
        public int Price { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
    }
}
