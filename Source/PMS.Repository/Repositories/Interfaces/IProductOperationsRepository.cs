using PMS.Repository.Entities.Product;

namespace PMS.Repository.Repositories.Interfaces
{
    /// <summary>
    /// Provides operations for managing product stock and searching products.
    /// </summary>
    public interface IProductOperationsRepository
    {
        /// <summary>
        /// Decrements the stock of a product by the specified quantity.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <param name="quantity">The quantity to decrement.</param>
        Task DecrementStock(int id, int quantity);

        /// <summary>
        /// Adds the specified quantity to the stock of a product.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <param name="quantity">The quantity to add.</param>
        Task AddToStock(int id, int quantity);

        /// <summary>
        /// Searches for products by name using a case-insensitive, partial match.
        /// </summary>
        /// <param name="name">The name or partial name to search for.</param>
        /// <returns>A collection of matching products.</returns>
        Task<IEnumerable<Product>> SearchProductsByName(string name);

        /// <summary>
        /// Retrieves products whose stock level is within the specified range.
        /// </summary>
        /// <param name="min">The minimum stock level (inclusive).</param>
        /// <param name="max">The maximum stock level (inclusive).</param>
        /// <returns>A collection of products within the specified stock range.</returns>
        Task<IEnumerable<Product>> GetProductsByStockLevel(int min, int max);
    }
}