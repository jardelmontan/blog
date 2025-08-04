using Blog.Domain.Enums;

namespace Blog.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string? Email { get; }
        bool TryGetRole(out UserRole role);
    }
}
