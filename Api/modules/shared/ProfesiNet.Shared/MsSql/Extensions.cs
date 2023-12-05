using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Configurations;

namespace ProfesiNet.Shared.MsSql;

public static class Extensions
{
    internal static IServiceCollection AddMsSql(this IServiceCollection services, string connectionString)
    {
        var options = services.GetOptions<MsSqlOptions>("mssql");
        services.AddSingleton(options);
        return services;
    }
    
    public static IServiceCollection AddMsSql<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<MsSqlOptions>("mssql");
        services.AddDbContext<T>(x =>
        {
            x.UseLazyLoadingProxies();
            x.UseSqlServer(options.ConnectionString);
        });
        
        return services;
    }
}

// services.AddDbContext<ProfesiNetLiveChatsDbContext>(options =>
// {
//     options
//         .UseLazyLoadingProxies()
//         .UseSqlServer(configuration.GetConnectionString("ProfesiNet"));
//
// });