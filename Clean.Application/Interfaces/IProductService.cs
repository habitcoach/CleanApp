﻿using Clean.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.Interfaces
{
    public interface IProductService
    {
        Task<Dto> GetProduct();
        public Task<ProductDto> GetProductById(ProductDto productdto);
    }
}
