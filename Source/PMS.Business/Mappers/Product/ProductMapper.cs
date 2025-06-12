using PMS.DTO.Product;

namespace PMS.Business.Mappers.Product
{
    /// <summary>
    /// Provides mapping methods between Product entities and DTOs.
    /// </summary>
    public static class ProductMapper
    {
        /// <summary>
        /// Maps a Product entity to a ProductDto.
        /// </summary>
        /// <param name="entity">The Product entity to map.</param>
        /// <returns>A ProductDto representing the entity.</returns>
        public static ProductDto ToProductDto(Repository.Entities.Product.Product entity)
        {
            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Stock = entity.Stock,
                CreationDate = entity.CreationDate,
                LastUpdated = entity.LastUpdated
            };
        }

        /// <summary>
        /// Maps a collection of Product entities to a collection of ProductDto objects.
        /// </summary>
        /// <param name="entities">The collection of Product entities to map.</param>
        /// <returns>An IEnumerable of ProductDto objects.</returns>
        public static IEnumerable<ProductDto> ToProductDtoList(
            IEnumerable<Repository.Entities.Product.Product> entities)
        {
            return entities.Select(ToProductDto);
        }

        /// <summary>
        /// Updates a Product entity with values from an UpdateProductRequestDto.
        /// </summary>
        /// <param name="entity">The Product entity to update.</param>
        /// <param name="dto">The DTO containing updated values.</param>
        public static void UpdateProductEntity(Repository.Entities.Product.Product entity, UpdateProductRequestDto dto)
        {
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.Stock = dto.Stock;
            entity.LastUpdated = DateTime.UtcNow;
        }

        /// <summary>
        /// Maps an AddProductRequestDto to a new Product entity for addition.
        /// </summary>
        /// <param name="dto">The DTO containing product creation data.</param>
        /// <returns>A new Product entity.</returns>
        public static Repository.Entities.Product.Product ToProductEntityForAddition(AddProductRequestDto dto)
        {
            return new Repository.Entities.Product.Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CreationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
        }
    }
}
