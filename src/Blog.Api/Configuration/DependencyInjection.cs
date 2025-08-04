using Blog.Api.Mappers;
using Blog.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using static Blog.Api.Constants;

namespace Blog.Api.Configuration
{
    public static class DependencyInjection
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlogDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString(ConfigurationKeys.DefaultConnectionString)
               ?? throw new InvalidOperationException("Connection string not found.")));

            services.AddAutoMapper(config =>
            {
                config.AddProfile<ErrorMappingProfile>();
            });

            return services;
        }
    }
}
