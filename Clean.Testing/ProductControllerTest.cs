using AutoMapper;
using Clean.Application.DTOs;
using Clean.Application.Interfaces;
using Clean.Application.Services;
using Clean.Domain.Core.Bus;
using Clean.Infra.Data.Context;
using Clean.Infra.Data.Repository;
using Clean.WebApi.Controllers;
using CleanDomain.Interfaces;
using CleanDomain.Models;
using Infra.Bus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Testing
{
    public class ProductControllerTest
    {
        private static DbContextOptions<RestaurantDBContext> dbContextOptions = new DbContextOptionsBuilder<RestaurantDBContext>()
           .UseInMemoryDatabase(databaseName: "RestaurantDBControllerTest")
           .Options;
     
        private ProductController productController;
        private Mock<IProductService> productServiceMock;

        [OneTimeSetUp] 
        public void Setup()
        {
            productServiceMock = new Mock<IProductService>();

            // Arrange
            productServiceMock.Setup(x => x.GetProduct("", "", "", true, 1, 10))
                .ReturnsAsync(new Dto
                {
                    Products = new List<Product> {
                 new Product { Id = 1,Name="Product01", Description="Desc for product01",
                    ImageUrl="imageUrl",Price= 999},
                new Product { Id = 2,Name="Product01", Description="Desc for product01",
                    ImageUrl="imageUrl",Price= 999},
                new Product { Id = 3,Name="Product01", Description="Desc for product01",
                    ImageUrl="imageUrl",Price= 999},
                new Product { Id = 4,Name="Product01", Description="Desc for product01",
                    ImageUrl="imageUrl",Price= 999}

                }
                });

            productController = new ProductController(productServiceMock.Object, new NullLogger<ProductController>());

        }

        [Test]
        public async Task HTTPGET_GetProductsV1_TestCount()
        {

            // Act
            IActionResult actionResult = await productController.GetProductsV1("", "", "", true,1,10);

            // Assert
            var actionResultData = (actionResult as OkObjectResult).Value as Dto;
            
            Assert.IsNotNull(actionResultData);
            Assert.AreEqual(actionResultData.Products.Count(), 4);
            Assert.That(actionResultData.Products.Count(), Is.EqualTo(4)); //better way

        }

        [Test]
        public async Task HTTPGET_GetProductsV1_TestName()
        {

            // Act
            IActionResult actionResult = await productController.GetProductsV1("", "", "", true, 1, 10);

            // Assert
            var actionResultData = (actionResult as OkObjectResult).Value as Dto;

            Assert.IsNotNull(actionResultData);
            Assert.AreEqual(actionResultData.Products.FirstOrDefault(x =>x.Id ==1).Name, "Product01");
            Assert.That(actionResultData.Products.FirstOrDefault(x => x.Id == 1).Name, Is.EqualTo("Product01")); //better way

        }

    }
}
