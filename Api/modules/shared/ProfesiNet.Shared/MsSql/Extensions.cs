using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Commands;
using ProfesiNet.Shared.Configurations;
using ProfesiNet.Shared.MsSql.Decorator;

namespace ProfesiNet.Shared.MsSql;

public static class Extensions
{
    internal static IServiceCollection AddMsSql(this IServiceCollection services)
    {
        var options = services.GetOptions<MsSqlOptions>("ConnectionStrings");
        services.AddSingleton(options);
        services.AddSingleton(new UnitOfWorkTypeRegistry());
        return services;
    }
    
    public static IServiceCollection AddMsSql<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<MsSqlOptions>("ConnectionStrings");
        services.AddDbContext<T>(x =>
        {
            x.UseLazyLoadingProxies();
            x.UseSqlServer(options.ProfesiNet);
        });
        
        return services;
    }
    
    public static IServiceCollection AddUnitOfWork<TUnitOfWork, TImplementation>(this IServiceCollection services) where TUnitOfWork :class, IUnitOfWork where TImplementation : class, TUnitOfWork
    {
        services.AddScoped<TUnitOfWork, TImplementation>();
        services.AddScoped<IUnitOfWork, TImplementation>();
        
        using var serviceProvider = services.BuildServiceProvider();
      serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<TUnitOfWork>();
        return services;
    }
    
    public static IServiceCollection AddTransactionalDecorator(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(TransactionalCommandDecorator<>));
        return services;
    }
}
