namespace PMS.Common.Configurations
{
    /// <summary>
    /// Defines the range of valid digits for ID values in the system.
    /// </summary>
    public static class IdValueDigits
    {
        public static readonly (int, int) SixDigits = (100000, 999999);
    }
}