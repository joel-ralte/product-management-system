using Moq;
using PMS.Business.Services;
using PMS.Business.UnitOfWork.Interfaces;
using PMS.Repository.Entities.Product;
using PMS.DTO.Product;
using PMS.Business.Services.Interfaces;
using PMS.Repository.Helpers.Interfaces;
using PMS.Repository.Repositories.Interfaces;

namespace PMS.Business.Tests.ServicesTests
{
    /// <summary>
    /// Unit tests for the ProductService to validate business logic and interactions with repositories.
    /// </summary>
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly Mock<IProductOperationsRepository> _productOpsRepoMock;
        private readonly Mock<IIdGeneratorHelper> _idGeneratorHelperMock;
        private readonly IProductService _service;

        public ProductServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productRepoMock = new Mock<IProductRepository>();
            _productOpsRepoMock = new Mock<IProductOperationsRepository>();
            _idGeneratorHelperMock = new Mock<IIdGeneratorHelper>();

            _unitOfWorkMock.SetupGet(u => u.ProductRepository).Returns(_productRepoMock.Object);
            _unitOfWorkMock.SetupGet(u => u.ProductOperationsRepository).Returns(_productOpsRepoMock.Object);

            _service = new ProductService(_unitOfWorkMock.Object, _idGeneratorHelperMock.Object);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsMappedDtos()
        {
            var products = new List<Product>
            {
                new()
                {
                    Id = 1, 
                    Name = "Sample", 
                    Description = "Desc", 
                    Price = 1, 
                    Stock = 1, 
                    CreationDate = DateTime.UtcNow, 
                    LastUpdated = DateTime.UtcNow
                }
            };
            _productRepoMock.Setup(r => r.GetAllProducts()).ReturnsAsync(products);

            var result = await _service.GetAllProducts();

            Assert.Single(result);
            Assert.Equal("Sample", result.First().Name);
        }

        [Fact]
        public async Task AddProduct_AddsAndReturnsDto()
        {
            var dto = new AddProductRequestDto
            {
                Name = "A", 
                Description = "D", 
                Price = 1, 
                Stock = 1
            };
            _productRepoMock.Setup(r => r.AddProduct(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _idGeneratorHelperMock.Setup(h => h.GenerateProductIdAsync()).ReturnsAsync(123);
            _unitOfWorkMock.Setup(u => u.SaveChanges(default)).ReturnsAsync(1);

            var result = await _service.AddProduct(dto);

            Assert.Equal(dto.Name, result.Name);
        }

        [Fact]
        public async Task GetProductById_ReturnsMappedDto_WhenFound()
        {
            var product = new Product
            {
                Id = 1, 
                Name = "A", 
                Description = "D", 
                Price = 1, 
                Stock = 1, 
                CreationDate = DateTime.UtcNow, 
                LastUpdated = DateTime.UtcNow
            };
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync(product);

            var result = await _service.GetProductById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetProductById_ReturnsNull_WhenNotFound()
        {
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync((Product?)null);

            var result = await _service.GetProductById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateProduct_Throws_WhenNotFound()
        {
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync((Product?)null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.UpdateProduct(1, new UpdateProductRequestDto
            {
                Name = "Test",
                Price = 10.0,
                Stock = 5
            }));
        }

        [Fact]
        public async Task UpdateProduct_UpdatesAndSaves_WhenFound()
        {
            var product = new Product
            {
                Id = 1, 
                Name = "A", 
                Description = "D", 
                Price = 1, 
                Stock = 1, 
                CreationDate = DateTime.UtcNow, 
                LastUpdated = DateTime.UtcNow
            };
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(u => u.SaveChanges(default)).ReturnsAsync(1);

            await _service.UpdateProduct(1, new UpdateProductRequestDto
            {
                Name = "B",
                Description = "E",
                Price = 2, 
                Stock = 2
            });
            _unitOfWorkMock.Verify(u => u.SaveChanges(default), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_DeletesAndSaves_WhenFound()
        {
            var product = new Product
            {
                Id = 1, 
                Name = "A", 
                Description = "D", 
                Price = 1, 
                Stock = 1, 
                CreationDate = DateTime.UtcNow, 
                LastUpdated = DateTime.UtcNow
            };
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(u => u.SaveChanges(default)).ReturnsAsync(1);

            await _service.DeleteProduct(1);

            _productRepoMock.Verify(r => r.DeleteProduct(product), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChanges(default), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_DoesNothing_WhenNotFound()
        {
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync((Product?)null);

            await _service.DeleteProduct(1);

            _productRepoMock.Verify(r => r.DeleteProduct(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChanges(default), Times.Never);
        }

        [Fact]
        public async Task DecrementStock_Throws_WhenNotFound()
        {
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync((Product?)null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.DecrementStock(1, 1));
        }

        [Fact]
        public async Task DecrementStock_Throws_WhenInsufficientStock()
        {
            var product = new Product
            {
                Id = 1, 
                Name = "A", 
                Description = "D", 
                Price = 1, 
                Stock = 0, 
                CreationDate = DateTime.UtcNow, 
                LastUpdated = DateTime.UtcNow
            };
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync(product);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.DecrementStock(1, 1));
        }

        [Fact]
        public async Task DecrementStock_CallsRepoAndSaves_WhenValid()
        {
            var product = new Product
            {
                Id = 1, 
                Name = "A", 
                Description = "D", 
                Price = 1,
                Stock = 10, 
                CreationDate = DateTime.UtcNow, 
                LastUpdated = DateTime.UtcNow
            };
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(u => u.SaveChanges(default)).ReturnsAsync(1);
            _productOpsRepoMock.Setup(r => r.DecrementStock(1, 1)).Returns(Task.CompletedTask);

            await _service.DecrementStock(1, 1);

            _productOpsRepoMock.Verify(r => r.DecrementStock(1, 1), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChanges(default), Times.Once);
        }

        [Fact]
        public async Task AddToStock_Throws_WhenNotFound()
        {
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync((Product?)null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddToStock(1, 1));
        }

        [Fact]
        public async Task AddToStock_CallsRepoAndSaves_WhenValid()
        {
            var product = new Product
            {
                Id = 1, 
                Name = "A",
                Description = "D", 
                Price = 1, 
                Stock = 10,
                CreationDate = DateTime.UtcNow, 
                LastUpdated = DateTime.UtcNow
            };
            _productRepoMock.Setup(r => r.GetProductById(1)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(u => u.SaveChanges(default)).ReturnsAsync(1);
            _productOpsRepoMock.Setup(r => r.AddToStock(1, 1)).Returns(Task.CompletedTask);

            await _service.AddToStock(1, 1);

            _productOpsRepoMock.Verify(r => r.AddToStock(1, 1), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChanges(default), Times.Once);
        }

        [Fact]
        public async Task SearchProductsByName_ReturnsMappedDtos()
        {
            var products = new List<Product>
            {
                new()
                {
                    Id = 1, 
                    Name = "A", 
                    Description = "D", 
                    Price = 1, 
                    Stock = 1, 
                    CreationDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                }
            };
            _productOpsRepoMock.Setup(r => r.SearchProductsByName("A")).ReturnsAsync(products);

            var result = await _service.SearchProductsByName("A");

            Assert.Single(result);
            Assert.Equal("A", result.First().Name);
        }

        [Fact]
        public async Task GetProductsByStockLevel_ReturnsMappedDtos()
        {
            var products = new List<Product>
            {
                new()
                {
                    Id = 1,
                    Name = "A",
                    Description = "D",
                    Price = 1, 
                    Stock = 1,
                    CreationDate = DateTime.UtcNow, 
                    LastUpdated = DateTime.UtcNow
                }
            };
            _productOpsRepoMock.Setup(r => r.GetProductsByStockLevel(0, 10)).ReturnsAsync(products);

            var result = await _service.GetProductsByStockLevel(0, 10);

            Assert.Single(result);
            Assert.Equal("A", result.First().Name);
        }
    }
}
