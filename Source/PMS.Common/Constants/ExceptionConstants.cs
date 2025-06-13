namespace PMS.Common.Constants
{
    /// <summary>
    /// Defines constants for exception messages used across the application.
    /// </summary>
    public static class ExceptionConstants
    {
        public const string ProductNotFound = "Product not found.";

        public const string InsufficientStock = "Insufficient stock. Please reduce decrement amount.";

        public const string StockExceedsLimit = "Resulting stock exceeds upper limit. Please reduce increment amount.";
    }
}