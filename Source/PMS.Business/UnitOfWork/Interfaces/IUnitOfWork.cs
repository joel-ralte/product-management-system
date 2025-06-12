using PMS.Repository.Repositories.Interfaces;

namespace PMS.Business.UnitOfWork.Interfaces
{
    /// <summary>
    /// Represents a unit of work that coordinates the work of multiple repositories
    /// and manages the saving of changes as a single transaction.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IProductOperationsRepository ProductOperationsRepository { get; }

        /// <summary>
        /// Saves all changes made in this unit of work to the database.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChanges(CancellationToken cancellationToken = default);
    }
}