namespace PMS.Common.Configurations
{
    /// <summary>
    /// Defines the range of valid values for product attributes such as price and stock.
    /// </summary>
    public static class RangeValues
    {
        public const double MinPrice = 0.0;

        public const double MaxPrice = 10000000.0;

        public const int MinStock = 1;

        public const int MaxStock = 5000;
    }
}
