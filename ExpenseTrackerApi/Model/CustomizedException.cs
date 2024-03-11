namespace ExpenseTrackerApi.Model
{
    public class CustomizedException : Exception
    {
        public int StatusCode { get; }
        public string CustomMessage { get; }
        public object AdditionalData { get; } // Additional object to return

        public CustomizedException(int statusCode, string message, object additionalData) : base(message)
        {
            StatusCode = statusCode;
            CustomMessage = message;
            AdditionalData = additionalData;
        }

        public CustomizedException(int statusCode, string message, object additionalData, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
            CustomMessage = message;
            AdditionalData = additionalData;
        }

        // You can add additional properties or methods as needed
    }
}
