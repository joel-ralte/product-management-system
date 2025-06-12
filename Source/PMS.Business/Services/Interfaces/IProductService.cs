using PMS.DTO.Product;

namespace PMS.Business.Services.Interfaces
{
    /// <summary>
    /// Provides product-related business operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of <see cref="ProductDto"/>.</returns>
        Task<IEnumerable<ProductDto>> GetAllProducts();

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">The product data to add.</param>
        /// <returns>The created <see cref="ProductDto"/>.</returns>
        Task<ProductDto> AddProduct(AddProductRequestDto product);

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns>The <see cref="ProductDto"/> if found; otherwise, null.</returns>
        Task<ProductDto?> GetProductById(int id);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="updatedProduct">The updated product data.</param>
        Task UpdateProduct(int id, UpdateProductRequestDto updatedProduct);

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        Task DeleteProduct(int id);

        /// <summary>
        /// Decrements the stock of a product.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="quantity">The quantity to decrement.</param>
        Task DecrementStock(int id, int quantity);

        /// <summary>
        /// Adds to the stock of a product.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="quantity">The quantity to add.</param>
        Task AddToStock(int id, int quantity);

        /// <summary>
        /// Searches for products by name.
        /// </summary>
        /// <param name="name">The name or partial name to search for.</param>
        /// <returns>A collection of matching <see cref="ProductDto"/>.</returns>
        Task<IEnumerable<ProductDto>> SearchProductsByName(string name);

        /// <summary>
        /// Retrieves products within a specified stock level range.
        /// </summary>
        /// <param name="min">The minimum stock level.</param>
        /// <param name="max">The maximum stock level.</param>
        /// <returns>A collection of <see cref="ProductDto"/> within the specified stock range.</returns>
        Task<IEnumerable<ProductDto>> GetProductsByStockLevel(int min, int max);
    }
}
