using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProfesiNetUserDbContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("ProfesiNet"));
        });
        return services;
    }
}