using Blog.Application.Features.Auth.Dtos;
using Blog.Application.Features.Auth.Interfaces;
using Blog.Application.Features.Auth.Mappers;
using Blog.Application.Features.Auth.Services;
using Blog.Application.Features.Posts.Interfaces;
using Blog.Application.Features.Posts.Mappers;
using Blog.Application.Features.Posts.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Blog.Application
{
    public static class DependencyInjection
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<UserProfile>();
                config.AddProfile<PostProfile>();
            });

            services.AddFluentValidationAutoValidation();

            // Registers all validators that are in the same assembly as the RegisterUserRequestValidator validator.
            // This includes all validators within the Features folder.
            services.AddValidatorsFromAssemblyContaining<RegisterUserRequest>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPostService, PostService>();

            return services;
        }
    }
}
