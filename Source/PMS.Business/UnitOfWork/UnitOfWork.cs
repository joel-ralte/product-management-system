using PMS.Business.UnitOfWork.Interfaces;
using PMS.Repository.DbContext;
using PMS.Repository.Repositories.Interfaces;

namespace PMS.Business.UnitOfWork
{
    /// <summary>
    /// Unit of Work implementation for managing repositories and database context.
    /// </summary>
    /// <param name="context">The database context used to interact with the database.</param>
    /// <param name="productRepository">The repository for managing product entities.</param>
    /// <param name="productOperationsRepository">The repository for managing product operations entities.</param>
    public class UnitOfWork(
        PmsDbContext context, 
        IProductRepository productRepository, 
        IProductOperationsRepository productOperationsRepository) 
        : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; } = productRepository;
        public IProductOperationsRepository ProductOperationsRepository { get; } = productOperationsRepository;

        /// <inheritdoc/>
        public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Disposes the unit of work, releasing the database context and repositories.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
