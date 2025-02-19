using App.Infrastructure.Interfaces;
using App.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace App.Infrastructure.Configuration
{
    public static class ConfigurationInfrastructureService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryBase, RepositoryBase>();
            return services;
        }

        public static IServiceCollection AddDbContextInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            return services;
        }

    }
}
