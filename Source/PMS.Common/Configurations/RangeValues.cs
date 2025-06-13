namespace PMS.Common.Configurations
{
    /// <summary>
    /// Defines the range of valid values for product attributes and validations.
    /// </summary>
    public static class RangeValues
    {
        /// <summary>
        /// Defines the minimum price for a product.
        /// </summary>
        public const double MinPrice = 0.0;

        /// <summary>
        /// Defines the maximum price for a product.
        /// </summary>
        public const double MaxPrice = 10000000.0;

        /// <summary>
        /// Defines the minimum stock level for a product.
        /// </summary>
        public const int MinStock = 1;

        /// <summary>
        /// Defines the maximum stock level for a product.
        /// </summary>
        public const int MaxStock = 50000;
    }
}
