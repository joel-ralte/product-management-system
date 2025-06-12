using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Repository.DbContext.Seeders;
using PMS.Repository.Entities.Product;

namespace PMS.Repository.DbContext.Configurations
{
    /// <summary>
    /// Configuration for the Product entity.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Configures the Product entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Id)
                .IsUnique();

            builder.HasData(ProductDataSeeder.GetProductSeeds());
        }
    }
}