using Blog.Application.Common.Interfaces;
using Blog.Application.Features.Auth.Interfaces;
using Blog.Application.Features.Auth.Services;
using Blog.Domain.Common.Interfaces;
using Blog.Domain.Interfaces;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Authentication;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Blog.Infrastructure
{
    public static class DependencyInjection
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
