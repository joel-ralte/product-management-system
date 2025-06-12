using PMS.Repository.DbContext;
using PMS.Repository.Entities.Product;
using Microsoft.EntityFrameworkCore;
using PMS.Repository.Repositories.Interfaces;

namespace PMS.Repository.Repositories
{
    /// <summary>
    /// Implementation class for the IProductRepository interface.
    /// </summary>
    /// <param name="context">The database context.</param>
    public class ProductRepository(PmsDbContext context) : IProductRepository
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await context.Products.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Product?> GetProductById(int id)
        {
            return await context.Products.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task AddProduct(Product entity)
        {
            await context.Products.AddAsync(entity);
        }

        /// <inheritdoc/>
        public void DeleteProduct(Product entity)
        {
            context.Products.Remove(entity);
        }
    }
}
