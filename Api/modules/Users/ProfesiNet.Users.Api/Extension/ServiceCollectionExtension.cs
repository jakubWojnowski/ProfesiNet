using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Users.Application.Configurations.Extensions;
using ProfesiNet.Users.Infrastructure.Extension;
[assembly: InternalsVisibleTo("ProfesiNetApi")]
namespace ProfesiNet.Users.Api.Extension;

internal static class ServiceCollectionExtension 
{
    public static IServiceCollection AddUserModule(this IServiceCollection services)
    {
        services.AddInfrastructure(services.BuildServiceProvider().GetService<IConfiguration>() ?? throw new InvalidOperationException());
        services.AddApplication();

        return services;

    }
}