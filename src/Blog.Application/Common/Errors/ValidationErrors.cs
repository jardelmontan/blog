using Blog.Domain.Enums;
using Blog.Domain.Errors;

namespace Blog.Application.Common.Errors
{
    public static class ValidationErrors
    {
        public static readonly ResultError Validation = new("validation", "One or more validation errors occurred.", ErrorType.Validation);
    }
}
