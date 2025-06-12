using Microsoft.EntityFrameworkCore;
using PMS.Repository.DbContext;
using PMS.Repository.Entities.Product;
using PMS.Repository.Repositories;

namespace PMS.Repository.Tests.RepositoryTests
{
    /// <summary>
    /// Unit tests for the ProductRepository class.
    /// </summary>
    public class ProductRepositoryTests
    {
        private static PmsDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<PmsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new PmsDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task AddProduct_ShouldAddProduct()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repo = new ProductRepository(context);
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

            // Act
            await repo.AddProduct(product);
            await context.SaveChangesAsync();

            // Assert
            var products = await context.Products.ToListAsync();
            var addedProduct = products.SingleOrDefault(p => p.Id == product.Id);
            Assert.NotNull(addedProduct);
            Assert.Equal("Test Product", addedProduct.Name);

        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Products.Add(new Product
            {
                Id = 2,
                Name = "Product 2",
                Description = "Desc 2",
                Price = 5.0,
                Stock = 5,
                CreationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            });
            context.Products.Add(new Product
            {
                Id = 3,
                Name = "Product 3",
                Description = "Desc 3",
                Price = 15.0,
                Stock = 15,
                CreationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
            var repo = new ProductRepository(context);

            // Act
            var products = await repo.GetAllProducts();

            // Assert
            var testProductIds = new[] { 2, 3 };
            Assert.Equal(2, products.Count(p => testProductIds.Contains(p.Id)));
        }

        [Fact]
        public async Task GetProductById_ShouldReturnCorrectProduct()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var product = new Product
            {
                Id = 4,
                Name = "Product 4",
                Description = "Desc 4",
                Price = 20.0,
                Stock = 20,
                CreationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();
            var repo = new ProductRepository(context);

            // Act
            var result = await repo.GetProductById(4);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Product 4", result!.Name);
        }

        [Fact]
        public async Task DeleteProduct_ShouldRemoveProduct()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var product = new Product
            {
                Id = 5,
                Name = "Product 5",
                Description = "Desc 5",
                Price = 30.0,
                Stock = 30,
                CreationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();
            var repo = new ProductRepository(context);

            // Act
            repo.DeleteProduct(product);
            await context.SaveChangesAsync();

            // Assert
            var result = await context.Products.FindAsync(5);
            Assert.Null(result);
        }
    }
}
