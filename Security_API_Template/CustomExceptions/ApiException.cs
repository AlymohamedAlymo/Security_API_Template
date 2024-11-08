namespace Security_API_Template.CustomExceptions
{
    public class ApiException(int statusCode, string message, string? details)
    {
        public int StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public string? Details { get; set; } = details;
    }
}
