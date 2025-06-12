namespace PMS.DTO.Product
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a product.
    /// </summary>
    public class ProductDto : BaseProductDto
    {
        public required int Id { get; set; }
        public required DateTime CreationDate { get; set; }
        public required DateTime LastUpdated { get; set; }
    }
}
