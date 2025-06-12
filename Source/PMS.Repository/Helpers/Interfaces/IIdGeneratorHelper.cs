namespace PMS.Repository.Helpers.Interfaces
{
    /// <summary>
    /// Interface for ID generation helper services.
    /// </summary>
    public interface IIdGeneratorHelper
    {
        /// <summary>
        /// Generates a unique product ID that is not already in use in the database.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the generated product ID as an integer.
        /// </returns>
        Task<int> GenerateProductIdAsync();
    }
}