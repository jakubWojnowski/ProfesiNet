
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.LiveChats.Core.DAL.Persistence;
using ProfesiNet.Shared.MsSql;

namespace ProfesiNet.LiveChats.Core.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        // services.AddDbContext<ProfesiNetLiveChatsDbContext>(options =>
        // {
        //     options
        //         .UseLazyLoadingProxies()
        //         .UseSqlServer(configuration.GetConnectionString("ProfesiNet"));
        //
        // });
        services.AddMsSql<ProfesiNetLiveChatsDbContext>();
        return services;
    }
}