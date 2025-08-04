namespace Blog.Application.Features.Auth.Dtos
{
    public class LoginUserRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
