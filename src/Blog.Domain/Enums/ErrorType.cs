namespace Blog.Domain.Enums
{
    public enum ErrorType
    {
        Validation = 400,
        Unauthorized = 401,
        NotFound = 404,
        BusinessRule = 422,
        Unexpected = 500,
    }
}
