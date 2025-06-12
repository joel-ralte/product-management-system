using Microsoft.EntityFrameworkCore;
using PMS.Repository.DbContext;
using PMS.Repository.Entities.Product;

namespace PMS.Repository.Tests.DbContextTests
{
    /// <summary>
    /// Tests for the PmsDbContext class.
    /// </summary>
    public class PmsDbContextTests
    {
        private PmsDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<PmsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new PmsDbContext(options);
        }

        [Fact]
        public void ProductsDbSet_ShouldExist()
        {
            using var context = CreateContext();

            Assert.NotNull(context.Products);
        }

        [Fact]
        public void CanAddAndRetrieveProduct()
        {
            using var context = CreateContext();
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Desc",
                Price = 9.99,
                Stock = 10,
                CreationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };

            context.Products.Add(product);
            context.SaveChanges();
            var retrieved = context.Products.FirstOrDefault(p => p.Id == 1);

            Assert.NotNull(retrieved);
            Assert.Equal("Test Product", retrieved!.Name);
        }
    }
}
