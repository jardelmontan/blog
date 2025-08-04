using Blog.Application.Features.Auth.Dtos;
using Blog.Domain.Common;

namespace Blog.Application.Features.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken);
        Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken);
    }
}
