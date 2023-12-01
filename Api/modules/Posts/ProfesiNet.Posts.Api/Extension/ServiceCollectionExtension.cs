using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Posts.Core.Extension;

namespace ProfesiNet.Posts.Api.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPostModule(this IServiceCollection services)
    {
        services.AddInfrastructure(services.BuildServiceProvider().GetService<IConfiguration>() ?? throw new InvalidOperationException());
        

        return services;
    }
}