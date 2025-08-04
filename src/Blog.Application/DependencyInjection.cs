using Blog.Application.Features.Auth.Dtos;
using Blog.Application.Features.Auth.Mappers;
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
            });

            services.AddFluentValidationAutoValidation();

            // Registers all validators that are in the same assembly as the RegisterUserRequestValidator validator.
            // This includes all validators within the Features folder.
            services.AddValidatorsFromAssemblyContaining<RegisterUserRequest>();

            return services;
        }
    }
}
