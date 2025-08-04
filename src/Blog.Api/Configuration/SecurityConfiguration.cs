using Blog.Domain.Authorization;
using Blog.Domain.Enums;
using Blog.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Blog.Api.Constants;

namespace Blog.Api.Configuration;

public static class Security
{
    public static IServiceCollection AddSecurityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        services.Configure<JwtSettings>(configuration.GetSection(ConfigurationKeys.JwtSettingsSection));
        var jwtSettings = configuration.GetSection(ConfigurationKeys.JwtSettingsSection).Get<JwtSettings>()
            ?? throw new InvalidOperationException("JwtSettings section not found in configuration.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret
                        ?? throw new InvalidOperationException("JWT Secret is not configured."))),

                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy(Policies.Author, policy => policy.RequireRole(UserRole.Author.ToString()));

        return services;
    }
}