using PMS.Repository.Entities.Product;

namespace PMS.Repository.Repositories.Interfaces
{
    /// <summary>
    /// Defines methods for accessing and managing products in the repository.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of products.</returns>
        Task<IEnumerable<Product>> GetAllProducts();

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddProduct(Product product);

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the product if found; otherwise, null.
        /// </returns>
        Task<Product?> GetProductById(int id);

        /// <summary>
        /// Deletes a product from the repository.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        void DeleteProduct(Product product);
    }
}