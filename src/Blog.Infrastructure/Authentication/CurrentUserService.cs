using Blog.Application.Common.Interfaces;
using Blog.Domain.Enums;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Blog.Infrastructure.Authentication
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public int UserId => int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public string? Email => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        public bool TryGetRole(out UserRole role)
        {
            var roleString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            return Enum.TryParse(roleString, ignoreCase: true, out role);
        }
    }
}
