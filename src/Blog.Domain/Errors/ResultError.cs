using Blog.Domain.Enums;

namespace Blog.Domain.Errors
{
    public class ResultError(string code, string message, ErrorType type, List<PropertyDetail>? properties = null)
    {
        public string Code { get; } = code;
        public string Message { get; } = message;
        public ErrorType Type { get; } = type;
        public List<PropertyDetail>? Properties { get; } = properties ?? [];

        public static ResultError NotFound(string code, string message) 
            =>  new(code, message, ErrorType.NotFound);

        public static ResultError BusinessRule(string code, string message) 
            => new(code, message, ErrorType.BusinessRule);

        public static ResultError Unauthorized(string code, string message) 
            => new(code, message, ErrorType.Unauthorized);

        public static ResultError Validation(string code, string message) 
            => new (code, message, ErrorType.Validation);

        public static ResultError Validation(string code, string message, List<PropertyDetail> properties) 
            => new(code, message, ErrorType.Validation, properties);
    }

    public record class PropertyDetail(string Name, string Message);
}
