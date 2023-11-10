using Clean.Application.Interfaces;
using Clean.Application.Services;
using Clean.Infra.Data.Repository;
using CleanDomain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection Services)
        {

            //Application layer

            Services.AddScoped<IProductService, ProductService>();

            //Infra.Data layer

            Services.AddScoped<IProductRepository, ProductRepository>();

        }
    }
}
