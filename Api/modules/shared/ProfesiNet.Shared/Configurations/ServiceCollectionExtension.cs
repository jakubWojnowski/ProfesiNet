using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Mediator;
using ProfesiNet.Shared.Middlewares.ValidatorBehaviors;
using ProfesiNet.Shared.Validators;

namespace ProfesiNet.Shared.Configurations;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddProfesiNetShared(this IServiceCollection services)
    {
        services.RegisterValidators();
        services.AddProfesiNetMediator();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}