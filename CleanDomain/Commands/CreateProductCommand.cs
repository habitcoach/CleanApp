using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDomain.Commands
{
    public class CreateProductCommand:ProductCommand
    {
        public CreateProductCommand(string name, string description, int price, string imageUrl)
        {
                Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
        }
    }
}
