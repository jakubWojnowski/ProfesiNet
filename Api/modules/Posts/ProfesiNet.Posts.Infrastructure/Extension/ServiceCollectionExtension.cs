using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Posts.Domain.Interfaces;
using ProfesiNet.Posts.Infrastructure.Persistence;
using ProfesiNet.Posts.Infrastructure.Repositories;

namespace ProfesiNet.Posts.Infrastructure.Extension;

public static class ServiceCollectionExtension 
{
    public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProfesiNetPostDbContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("ProfesiNet"));
        });
        services.AddScoped(typeof(GenericRepository<,>));
        services.AddScoped<IPostRepository, PostRepository>();
        return services;
    }
}