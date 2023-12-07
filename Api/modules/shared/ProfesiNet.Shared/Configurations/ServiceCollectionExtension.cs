using System.Reflection;
using System.Runtime.CompilerServices;
using Confab.Shared.Abstractions.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Api;
using ProfesiNet.Shared.Mediator;
using ProfesiNet.Shared.Middlewares;
using ProfesiNet.Shared.Modules;
using ProfesiNet.Shared.MsSql;
using ProfesiNet.Shared.Services;
using ProfesiNet.Shared.Time;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Shared.Validators;
using ProfesiNet.Shared.Validators.ValidatorBehaviors;

[assembly: InternalsVisibleTo("ProfesiNetApi")]
namespace ProfesiNet.Shared.Configurations;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IList<Assembly> assemblies, IList<IModule> models)
    {
        var disabledModules = new List<string>();
        using (var servicesProvider = services.BuildServiceProvider())
        {
            var configuration = servicesProvider.GetRequiredService<IConfiguration>();
            foreach (var (key,value) in configuration.AsEnumerable())
            {
                if(!key.Contains(":module:enabled"))
                    continue;
                if (!bool.Parse(value))
                {
                    disabledModules.Add(key.Split(":")[0]);
                }
            }

          
        }
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
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x =>
                        x.Name.Contains(disabledModule, StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });
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
