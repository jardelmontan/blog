namespace Blog.Api.Models
{
    public class ErrorResponse(string code, string message, List<PropertyErrorDetail>? properties = null)
    {
        public string Code { get; } = code;
        public string Message { get; } = message;
        public List<PropertyErrorDetail>? Properties { get; set; } = properties ?? [];
    }

    public record class PropertyErrorDetail(string Name, string Message);
}
