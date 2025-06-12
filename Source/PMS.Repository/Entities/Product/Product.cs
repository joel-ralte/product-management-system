using System.ComponentModel.DataAnnotations;

namespace PMS.Repository.Entities.Product
{
    /// <summary>
    /// Represents a product in the Product Management System (PMS).
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(64)]
        public required string Name { get; set; }
        
        [MaxLength(128)]
        public string? Description { get; set; }
        public double Price { get; set; }
        public required int Stock { get; set; }
        public required DateTime CreationDate { get; set; }
        public required DateTime LastUpdated { get; set; } 
    }
}
