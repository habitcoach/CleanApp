using Clean.Infra.Data.Context;
using Clean.Infra.Data.Repository;
using CleanDomain.Interfaces;
using CleanDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace Clean.Testing
{
    public class ProductRepositoryTest

    {

        private static DbContextOptions<RestaurantDBContext> dbContextOptions = new DbContextOptionsBuilder<RestaurantDBContext>()
            .UseInMemoryDatabase(databaseName: "RestaurantDBTest")
            .Options;
        //Options builder is used to change the dboption from sqlserver to inmemorydb

        RestaurantDBContext context; //Instance of inmemory db
        IProductRepository productRepository;

        [OneTimeSetUp]   // one time set up database
        public void Setup()
        {
            context = new RestaurantDBContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();

            productRepository = new ProductRepository(context);
        }

        [Test,Order(1)]  //order define the order of the test

        public async Task GetAllProducts() {

            List<Product> result = await productRepository.GetProduct("","","");

           // Assert.That(result.Count, Is.EqualTo(1));
            Assert.AreEqual(result.Count, 4);
                
        
        }
        [Test,Order(2)]
        public async Task GetAllProductById()
        {

            Product result = await productRepository.GetProductById(1);

            // Assert.That(result.Count, Is.EqualTo(1));
            Assert.AreEqual(result.Name, "Product01");


        }





        [OneTimeTearDown]
        public void CleanUp() { 
        
            context.Database.EnsureDeleted();

        }

        private void SeedDatabase()
        {

            var products = new List<Product>
            {
                new Product { Id = 1,Name="Product01", Description="Desc for product01",
                    ImageUrl="imageUrl",Price= 999},
                new Product { Id = 2,Name="Product01", Description="Desc for product01",
                    ImageUrl="imageUrl",Price= 999},
                new Product { Id = 3,Name="Product01", Description="Desc for product01",
                    ImageUrl="imageUrl",Price= 999},
                new Product { Id = 4,Name="Product01", Description="Desc for product01",
                    ImageUrl="imageUrl",Price= 999}



            };
            context.Products.AddRange(products);
            context.SaveChanges();

        }

        
    }
}