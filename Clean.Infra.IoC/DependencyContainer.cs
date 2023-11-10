using Clean.Application.Interfaces;
using Clean.Application.Services;
using Clean.Domain.Core.Bus;
using Clean.Infra.Data.Repository;
using CleanDomain.CommandQueryHandlers;
using CleanDomain.Interfaces;
using CleanDomain.Models;
using CleanDomain.Queries;
using Infra.Bus;
using MediatR;
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

            //Bus
            Services.AddScoped<IMediatorHandler, InMemoryBus>();

            Services.AddScoped<IRequestHandler<GetProductsQuery, IEnumerable<Product>>, GetProductsQueryHandler>();



        }
    }
}
