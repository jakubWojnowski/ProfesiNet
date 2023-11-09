using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Users.Infrastructure.Extension;

namespace ProfesiNet.Users.Api.Extension;

public static class ServiceCollectionExtension 
{
    public static IServiceCollection AddUserModule(this IServiceCollection services)
    {
        services.AddInfrastructure(services.BuildServiceProvider().GetService<IConfiguration>() ?? throw new InvalidOperationException());

        return services;

    }
}