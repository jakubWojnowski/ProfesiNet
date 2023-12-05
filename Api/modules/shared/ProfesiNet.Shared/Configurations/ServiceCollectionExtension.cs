using Confab.Shared.Abstractions.Interfaces;
using Confab.Shared.Infrastructure.Api;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Mediator;
using ProfesiNet.Shared.Middlewares;
using ProfesiNet.Shared.MsSql;
using ProfesiNet.Shared.Services;
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
        services.AddMsSql();
        services.RegisterValidators();
        services.AddProfesiNetMediator();
        services.AddControllers();
        services.AddSingleton<IClock, UtcClock>();
        services.AddHostedService<ApiInitializer>();
       
        //services.AddExceptionHandler<ExceptionHandler>();
        services.AddScoped<ICurrentUserContextService, CurrentUserContextService>();
        services.AddSingleton<ITokenRevocationListService, TokenRevocationListService>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider()
                ));
        return services;
    }
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseErrorHandling();
        // app.UseHttpsRedirection();
        // app.UseRouting();
        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapControllers();
        //     endpoints.MapGet("/", context => context.Response.WriteAsync("ProfesiNet!!!!"));
        // });
        
        return app;
    }
    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);

    }

    private static T GetOptions<T>(this IConfiguration configuration, string sectionName)
        where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}
