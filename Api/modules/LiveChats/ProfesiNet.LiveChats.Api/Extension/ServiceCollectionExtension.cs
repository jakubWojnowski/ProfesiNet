using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.LiveChats.Core.Extension;

namespace ProfesiNet.LiveChats.Api.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddLiveChatsModule(this IServiceCollection services)
    {
        services.AddInfrastructure(services.BuildServiceProvider().GetService<IConfiguration>() ?? throw new InvalidOperationException());
        return services;
    }   
}