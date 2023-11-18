using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Users.Application.Configurations.ValidatorConfigurations;

namespace ProfesiNet.Users.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        services.RegisterValidators();
        return services;
    }
}