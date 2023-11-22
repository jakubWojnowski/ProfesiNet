using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.LiveChats.Infrastructure.Persistence;

namespace ProfesiNet.LiveChats.Infrastructure.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProfesiNetLiveChatsDbContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("ProfesiNet"));

        });
        return services;
    }
}