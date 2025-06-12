using Microsoft.EntityFrameworkCore;
using PMS.Common.Configurations;
using PMS.Repository.DbContext;
using PMS.Repository.Helpers.Interfaces;

namespace PMS.Repository.Helpers
{
    /// <summary>
    /// Helper class for generating unique IDs for products in the Product Management System (PMS).
    /// </summary>
    /// <param name="context">The database context used to access the product data.</param>
    public class IdGeneratorHelper(PmsDbContext context) : IIdGeneratorHelper
    {
        private static readonly Random Random = new();

        /// <inheritdoc/>
        public async Task<int> GenerateProductIdAsync()
        {
            int id;
            do
            {
                id = Random.Next(IdValueDigits.SixDigits.Item1, IdValueDigits.SixDigits.Item2);
            }
            while (await context.Products.AnyAsync(e => e.Id == id));
            return id;
        }
    }
}