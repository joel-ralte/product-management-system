using Microsoft.EntityFrameworkCore;
using PMS.Common.Configurations;
using PMS.Repository.DbContext;
using PMS.Repository.Entities.Product;
using PMS.Repository.Helpers;

namespace PMS.Repository.Tests.HelperTests
{
    /// <summary>
    /// Unit tests for the IdGeneratorHelper class.
    /// </summary>
    public class IdGeneratorHelperTests
    {
        private PmsDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<PmsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new PmsDbContext(options);
        }

        [Fact]
        public async Task GenerateProductIdAsync_ReturnsSixDigitId()
        {
            // Arrange
            await using var context = GetInMemoryDbContext();
            var helper = new IdGeneratorHelper(context);

            // Act
            var id = await helper.GenerateProductIdAsync();

            // Assert
            Assert.InRange(id, IdValueDigits.SixDigits.Item1, IdValueDigits.SixDigits.Item2);
        }

        [Fact]
        public async Task GenerateProductIdAsync_ReturnsUniqueId()
        {
            // Arrange
            await using var context = GetInMemoryDbContext();
            // Add a product with a known ID
            var existingProduct = new Product
            {
                Id = 123456,
                Name = "Test",
                Description = "Test",
                Price = 1,
                Stock = 1,
                CreationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
            context.Products.Add(existingProduct);
            await context.SaveChangesAsync();

            var helper = new IdGeneratorHelper(context);

            // Act
            var id = await helper.GenerateProductIdAsync();

            // Assert
            Assert.NotEqual(existingProduct.Id, id);
        }
    }
}