using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Mediator;
using ProfesiNet.Shared.Middlewares;
using ProfesiNet.Shared.Validators;
using ProfesiNet.Shared.Validators.ValidatorBehaviors;
using ProfesiNet.Users.Application.UserContext;

namespace ProfesiNet.Shared.Configurations;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddProfesiNetShared(this IServiceCollection services)
    {
        services.RegisterValidators();
        services.AddProfesiNetMediator();
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddScoped<ICurrentUserContextService, CurrentUserContextService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}