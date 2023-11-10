using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDomain.Commands
{
    public class UpdateProductCommand:ProductCommand
    {
        public int Id { get; set; }
        public UpdateProductCommand(int id, string name, string description, int price, string imageUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
        }
    }
}
