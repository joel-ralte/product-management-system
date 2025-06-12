using Microsoft.EntityFrameworkCore;
using PMS.Repository.DbContext;
using PMS.Repository.Entities.Product;
using PMS.Repository.Repositories;

namespace PMS.Repository.Tests.RepositoryTests
{
    /// <summary>
    /// Unit tests for the ProductOperationsRepository class.
    /// </summary>
    public class ProductOperationsRepositoryTests
    {
        private static PmsDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<PmsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new PmsDbContext(options);
            context.Products.Add(new Product
            {
                Id = 1,
                Name = "TestProduct",
                Description = "Desc",
                Price = 10,
                Stock = 5,
                CreationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            });
            context.Products.Add(new Product
            {
                Id = 2,
                Name = "AnotherProduct",
                Description = "Desc2",
                Price = 20,
                Stock = 15,
                CreationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            });
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task DecrementStock_DecreasesStockAndUpdatesLastUpdated()
        {
            var context = GetInMemoryDbContext();
            var repo = new ProductOperationsRepository(context);

            await repo.DecrementStock(1, 2);
            var product = await context.Products.FindAsync(1);

            Assert.NotNull(product); 
            Assert.Equal(3, product.Stock);
            Assert.True((DateTime.UtcNow - product.LastUpdated).TotalSeconds < 5);
        }

        [Fact]
        public async Task AddToStock_IncreasesStockAndUpdatesLastUpdated()
        {
            var context = GetInMemoryDbContext();
            var repo = new ProductOperationsRepository(context);

            await repo.AddToStock(1, 4);
            var product = await context.Products.FindAsync(1);

            Assert.NotNull(product);
            Assert.Equal(9, product.Stock);
            Assert.True((DateTime.UtcNow - product.LastUpdated).TotalSeconds < 5);
        }

        [Fact]
        public async Task SearchProductsByName_ReturnsCaseInsensitivePartialMatches()
        {
            var context = GetInMemoryDbContext();
            var repo = new ProductOperationsRepository(context);

            var results = await repo.SearchProductsByName("test");
            Assert.Single(results);
            Assert.Equal("TestProduct", results.First().Name);

            results = await repo.SearchProductsByName("PRODUCT");
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetProductsByStockLevel_ReturnsCorrectProducts()
        {
            var context = GetInMemoryDbContext();
            var repo = new ProductOperationsRepository(context);

            var results = await repo.GetProductsByStockLevel(1, 10);
            Assert.Single(results);
            Assert.Equal("TestProduct", results.First().Name);

            results = await repo.GetProductsByStockLevel(10, 20);
            Assert.Single(results);
            Assert.Equal("AnotherProduct", results.First().Name);
        }
    }
}
