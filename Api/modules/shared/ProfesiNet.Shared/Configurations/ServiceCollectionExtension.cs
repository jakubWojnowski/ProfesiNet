using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Mediator;
using ProfesiNet.Shared.Validators;
using ProfesiNet.Shared.Validators.ValidatorBehaviors;

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