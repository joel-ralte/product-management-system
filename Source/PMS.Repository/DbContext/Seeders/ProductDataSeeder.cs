using PMS.Repository.Entities.Product;

namespace PMS.Repository.DbContext.Seeders
{
    /// <summary>
    /// Seeder class for populating initial product data in the database.
    /// </summary>
    public static class ProductDataSeeder
    {
        /// <summary>
        /// Gets a collection of product seeds to be used for database seeding.
        /// </summary>
        /// <returns>
        /// An enumerable collection of <see cref="Product"/> objects representing initial product data.
        /// </returns>
        public static IEnumerable<Product> GetProductSeeds()
        {
            return new List<Product>
               {
                   new()
                   {
                       Id = 100001,
                       Name = "Sample Product",
                       Description = "Test product for demonstration.",
                       Price = 10,
                       Stock = 100,
                       CreationDate = DateTime.UtcNow.AddDays(-10),
                       LastUpdated = DateTime.UtcNow.AddDays(-9)
                   },
                   new()
                   {
                       Id = 100002,
                       Name = "Laptop Pro",
                       Description = "High-end laptop for professionals.",
                       Price = 1500,
                       Stock = 25,
                       CreationDate = DateTime.UtcNow.AddDays(-30),
                       LastUpdated = DateTime.UtcNow.AddDays(-20)
                   },
                   new()
                   {
                       Id = 100003,
                       Name = "Wireless Mouse",
                       Description = "Ergonomic wireless mouse.",
                       Price = 25,
                       Stock = 200,
                       CreationDate = DateTime.UtcNow.AddDays(-5),
                       LastUpdated = DateTime.UtcNow
                   },
                   new()
                   {
                       Id = 100004,
                       Name = "Office Chair",
                       Description = "Comfortable office chair with lumbar support.",
                       Price = 120,
                       Stock = 50,
                       CreationDate = DateTime.UtcNow.AddDays(-20),
                       LastUpdated = DateTime.UtcNow.AddDays(-10)
                   },
                   new()
                   {
                       Id = 100005,
                       Name = "Gaming Keyboard",
                       Description = "Mechanical keyboard with RGB lighting.",
                       Price = 80,
                       Stock = 0,
                       CreationDate = DateTime.UtcNow.AddDays(-15),
                       LastUpdated = DateTime.UtcNow.AddDays(-10)
                   },
                   new()
                   {
                       Id = 100006,
                       Name = "USB-C Hub",
                       Description = "Multi-port USB-C hub for laptops.",
                       Price = 45,
                       Stock = 75,
                       CreationDate = DateTime.UtcNow.AddDays(-2),
                       LastUpdated = DateTime.UtcNow
                   },
                   new()
                   {
                       Id = 100007,
                       Name = "Noise Cancelling Headphones",
                       Description = "Over-ear headphones with active noise cancellation.",
                       Price = 200,
                       Stock = 10,
                       CreationDate = DateTime.UtcNow.AddDays(-40),
                       LastUpdated = DateTime.UtcNow.AddDays(-1)
                   }
               };
        }
    }
}
