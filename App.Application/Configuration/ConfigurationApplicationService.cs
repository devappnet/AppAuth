using App.Application.Interfaces;
using App.Application.Services;
using App.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application.Configuration
{
    /// <summary>
    /// Configuration application service
    /// </summary>
    public static class ConfigurationApplicationService
    {
        /// <summary>
        /// Add service configuration
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<AspNetUser>, PasswordHasher<AspNetUser>>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
