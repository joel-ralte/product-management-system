using PMS.Common.Configurations;
using System.ComponentModel.DataAnnotations;

namespace PMS.DTO.Product
{
    /// <summary>
    /// Base class for product data transfer objects (DTOs).
    /// </summary>
    public class BaseProductDto
    {
        [MaxLength(64)]
        public required string Name { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }

        [Range(RangeValues.MinPrice, RangeValues.MaxPrice)]
        public required double Price { get; set; }

        [Range(RangeValues.MinStock, RangeValues.MaxStock)]
        public required int Stock { get; set; }
    }
}
