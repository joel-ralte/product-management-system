using PMS.Business.Services.Interfaces;
using PMS.Business.UnitOfWork.Interfaces;
using PMS.Business.Mappers.Product;
using PMS.Common.Constants;
using PMS.DTO.Product;
using PMS.Repository.Helpers.Interfaces;

namespace PMS.Business.Services
{
    /// <summary>
    /// Service for managing products in the Product Management System (PMS).
    /// </summary>
    /// <param name="unitOfWork">The unit of work instance for managing database operations. </param>
    /// <param name="idGenerator">The ID generator helper for generating unique product IDs. </param>
    public class ProductService(IUnitOfWork unitOfWork, IIdGeneratorHelper idGenerator) : IProductService
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await unitOfWork.ProductRepository.GetAllProducts();
            return ProductMapper.ToProductDtoList(products);
        }

        /// <inheritdoc/>
        public async Task<ProductDto> AddProduct(AddProductRequestDto productDto)
        {
            var product = ProductMapper.ToProductEntityForAddition(productDto);
            product.Id = await idGenerator.GenerateProductIdAsync();

            await unitOfWork.ProductRepository.AddProduct(product);
            await unitOfWork.SaveChanges();

            return ProductMapper.ToProductDto(product);
        }

        /// <inheritdoc/>
        public async Task<ProductDto?> GetProductById(int id)
        {
            var product = await unitOfWork.ProductRepository.GetProductById(id);

            return product is null ? null : ProductMapper.ToProductDto(product);
        }

        /// <inheritdoc/>
        public async Task UpdateProduct(int id, UpdateProductRequestDto updatedProduct)
        {
            var existingProduct = await unitOfWork.ProductRepository.GetProductById(id);
            if (existingProduct == null)
                throw new InvalidOperationException(ExceptionConstants.ProductNotFound);

            ProductMapper.UpdateProductEntity(existingProduct, updatedProduct);

            await unitOfWork.SaveChanges();
        }

        /// <inheritdoc/>
        public async Task DeleteProduct(int id)
        {
            var product = await unitOfWork.ProductRepository.GetProductById(id);
            if (product != null)
            {
                unitOfWork.ProductRepository.DeleteProduct(product);
                await unitOfWork.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public async Task DecrementStock(int id, int quantity)
        {
            var product = await unitOfWork.ProductRepository.GetProductById(id);
            if (product == null)
                throw new InvalidOperationException(ExceptionConstants.ProductNotFound);

            if (product.Stock < quantity)
                throw new InvalidOperationException(ExceptionConstants.InsufficientStock);

            await unitOfWork.ProductOperationsRepository.DecrementStock(id, quantity);
            await unitOfWork.SaveChanges();
        }

        /// <inheritdoc/>
        public async Task AddToStock(int id, int quantity)
        {
            var product = await unitOfWork.ProductRepository.GetProductById(id);
            if (product == null)
                throw new InvalidOperationException(ExceptionConstants.ProductNotFound);

            await unitOfWork.ProductOperationsRepository.AddToStock(id, quantity);
            await unitOfWork.SaveChanges();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProductDto>> SearchProductsByName(string name)
        {
            var products = await unitOfWork.ProductOperationsRepository.SearchProductsByName(name);
            return products.Select(ProductMapper.ToProductDto);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProductDto>> GetProductsByStockLevel(int min, int max)
        {
            var products = await unitOfWork.ProductOperationsRepository.GetProductsByStockLevel(min, max);
            return products.Select(ProductMapper.ToProductDto);
        }
    }
}
