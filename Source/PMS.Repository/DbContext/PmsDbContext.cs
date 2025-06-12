using Microsoft.EntityFrameworkCore;
using PMS.Repository.DbContext.Configurations;
using PMS.Repository.Entities.Product;

namespace PMS.Repository.DbContext
{
    /// <summary>
    /// Database context for the Product Management System (PMS).
    /// </summary>
    /// <param name="options">
    /// The options to configure the database context, such as connection strings and provider settings.
    /// </param>
    public class PmsDbContext(DbContextOptions<PmsDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();

        /// <summary>
        /// Configures the model for the Product Management System (PMS) database context.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the entity types and relationships.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}