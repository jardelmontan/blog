using Blog.Domain.Enums;
using Blog.Domain.Errors;

namespace Blog.Application.Common.Errors
{
    public static class AuthErrors
    {
        public static readonly ResultError UserDoesNotExist = new("user_does_not_exist", "User does not exist.", ErrorType.NotFound);
        public static readonly ResultError InvalidCredentials = new("invalid_credentials", "Invalid email or password.", ErrorType.Unauthorized);
        public static readonly ResultError EmailAlreadyRegistered = new("email_already_registered", "The email is already registered.", ErrorType.BusinessRule);
    }
}
