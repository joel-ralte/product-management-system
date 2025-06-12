using Microsoft.AspNetCore.Mvc;
using Moq;
using PMS.Business.Services.Interfaces;
using PMS.DTO.Product;
using PMS.WebAPI.Controllers;

namespace PMS.WebAPI.Tests.ControllerTests
{
    /// <summary>
    /// Unit tests for the ProductController to validate model state and service interactions.
    /// </summary>
    public class ProductControllerValidationTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerValidationTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public async Task AddProduct_InvalidModel_ReturnsBadRequest()
        {
            var invalidDto = new AddProductRequestDto
            {
                Name = null!,
                Price = 10,
                Stock = 5,
                Description = "desc"
            };
            _controller.ModelState.AddModelError("Name", "The Name field is required.");

            var result = await _controller.AddProduct(invalidDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AddProduct_NegativePrice_ReturnsBadRequest()
        {
            var invalidDto = new AddProductRequestDto
            {
                Name = "Test",
                Price = -1,
                Stock = 5,
                Description = "desc"
            };
            _controller.ModelState.AddModelError("Price", "The field Price must be between 0 and ...");

            var result = await _controller.AddProduct(invalidDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_InvalidModel_ReturnsBadRequest()
        {
            var invalidDto = new UpdateProductRequestDto
            {
                Name = "",
                Price = 10,
                Stock = 5,
                Description = "desc"
            };
            _controller.ModelState.AddModelError("Name", "The Name field is required.");

            var result = await _controller.UpdateProduct(1, invalidDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_NegativeStock_ReturnsBadRequest()
        {
            var invalidDto = new UpdateProductRequestDto
            {
                Name = "Test",
                Price = 10,
                Stock = -5,
                Description = "desc"
            };
            _controller.ModelState.AddModelError("Stock", "The field Stock must be between 0 and ...");

            var result = await _controller.UpdateProduct(1, invalidDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AddProduct_ValidModel_ReturnsCreatedAtAction()
        {
            var validDto = new AddProductRequestDto
            {
                Name = "Test",
                Price = 10,
                Stock = 5,
                Description = "desc"
            };
            var product = new ProductDto
            {
                Id = 123,
                Name = "Test",
                Description = "desc",
                Price = 10,
                Stock = 5,
                CreationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
            _mockService.Setup(s => s.AddProduct(validDto)).ReturnsAsync(product);

            var result = await _controller.AddProduct(validDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(product, createdResult.Value);
        }

        [Fact]
        public async Task UpdateProduct_ProductNotFound_ReturnsNotFound()
        {
            var updateDto = new UpdateProductRequestDto
            {
                Name = "Updated",
                Price = 20,
                Stock = 10,
                Description = "desc"
            };
            _mockService.Setup(s => s.GetProductById(1)).ReturnsAsync((ProductDto?)null);

            var result = await _controller.UpdateProduct(1, updateDto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ProductNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetProductById(1)).ReturnsAsync((ProductDto?)null);

            var result = await _controller.DeleteProduct(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetProductById_ProductNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetProductById(1)).ReturnsAsync((ProductDto?)null);

            var result = await _controller.GetProductById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkWithProducts()
        {
            var products = new List<ProductDto>
            {
                new()
                {
                    Id = 1,
                    Name = "Test",
                    Price = 10,
                    Stock = 5,
                    CreationDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                }
            };
            _mockService.Setup(s => s.GetAllProducts()).ReturnsAsync(products);

            var result = await _controller.GetAllProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(products, okResult.Value);
        }

        [Fact]
        public async Task SearchProductsByName_ReturnsOkWithProducts()
        {
            var products = new List<ProductDto>
            {
                new()
                {
                    Id = 1,
                    Name = "Test",
                    Price = 10,
                    Stock = 5,
                    CreationDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                }
            };
            _mockService.Setup(s => s.SearchProductsByName("Test")).ReturnsAsync(products);

            var result = await _controller.SearchProductsByName("Test");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(products, okResult.Value);
        }

        [Fact]
        public async Task GetProductsByStockLevel_ReturnsOkWithProducts()
        {
            var products = new List<ProductDto>
            {
                new()
                {
                    Id = 1,
                    Name = "Test",
                    Price = 10,
                    Stock = 5,
                    CreationDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                }
            };
            _mockService.Setup(s => s.GetProductsByStockLevel(1, 10)).ReturnsAsync(products);

            var result = await _controller.GetProductsByStockLevel(1, 10);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(products, okResult.Value);
        }
    }
}
