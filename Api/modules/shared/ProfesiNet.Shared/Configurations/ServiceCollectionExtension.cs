using Confab.Shared.Abstractions.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Mediator;
using ProfesiNet.Shared.Middlewares;
using ProfesiNet.Shared.Time;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Shared.Validators;
using ProfesiNet.Shared.Validators.ValidatorBehaviors;

namespace ProfesiNet.Shared.Configurations;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddErrorHandling();
        services.RegisterValidators();
        services.AddProfesiNetMediator();
        services.AddControllers();
        services.AddSingleton<IClock, UtcClock>();
       
        //services.AddExceptionHandler<ExceptionHandler>();
        services.AddScoped<ICurrentUserContextService, CurrentUserContextService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseErrorHandling();
        
        return app;
    }
}
