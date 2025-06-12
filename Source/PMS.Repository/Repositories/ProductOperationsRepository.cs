using Microsoft.EntityFrameworkCore;
using PMS.Repository.DbContext;
using PMS.Repository.Entities.Product;
using PMS.Repository.Repositories.Interfaces;

namespace PMS.Repository.Repositories
{
    /// <summary>
    /// Implementation class for the IProductOperationsRepository interface.
    /// </summary>
    /// <param name="context">The database context.</param>
    public class ProductOperationsRepository(PmsDbContext context) : IProductOperationsRepository
    {
        /// <inheritdoc/>
        public async Task DecrementStock(int id, int quantity)
        {
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                product.Stock -= quantity;
                product.LastUpdated = DateTime.UtcNow;
            }
        }

        /// <inheritdoc/>
        public async Task AddToStock(int id, int quantity)
        {
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                product.Stock += quantity;
                product.LastUpdated = DateTime.UtcNow;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> SearchProductsByName(string name)
        {
            return await context.Products
                .Where(p => !string.IsNullOrEmpty(p.Name) &&
                            EF.Functions.Like(p.Name, $"%{name}%"))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetProductsByStockLevel(int min, int max)
        {
            return await context.Products
                .Where(p => p.Stock >= min && p.Stock <= max)
                .ToListAsync();
        }
    }
}