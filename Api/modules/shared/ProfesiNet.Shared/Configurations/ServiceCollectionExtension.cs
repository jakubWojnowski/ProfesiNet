using System.Reflection;
using System.Runtime.CompilerServices;
using Confab.Shared.Abstractions.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProfesiNet.Shared.Api;
using ProfesiNet.Shared.Contexts;
using ProfesiNet.Shared.Events;
using ProfesiNet.Shared.Mediator;
using ProfesiNet.Shared.Messaging;
using ProfesiNet.Shared.Middlewares;
using ProfesiNet.Shared.Modules;
using ProfesiNet.Shared.MsSql;
using ProfesiNet.Shared.Photos;
using ProfesiNet.Shared.Services;
using ProfesiNet.Shared.Time;
using ProfesiNet.Shared.Validators;
using ProfesiNet.Shared.Validators.ValidatorBehaviors;

[assembly: InternalsVisibleTo("ProfesiNetApi")]
namespace ProfesiNet.Shared.Configurations;

internal static class ServiceCollectionExtension
{
    private const string CorsPolicy = "cors";

    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IList<Assembly> assemblies, IList<IModule> modules, IConfiguration config)
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
        services.AddCors(cors =>
        {
            cors.AddPolicy(CorsPolicy, x =>
            {
                x.WithOrigins("http://localhost:3000")
                    .WithMethods("POST","GET", "PUT", "DELETE", "PATCH")
                    .WithHeaders("Content-Type", "Authorization");
            });
        });
        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ProfesiNet API",
                Version = "v1"
            });
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        services.AddErrorHandling();
        services.AddMsSql();
        services.RegisterValidators();
        services.AddProfesiNetMediator();
        services.AddControllers();
        services.AddSingleton<IClock, UtcClock>();
        services.AddHostedService<ApiInitializer>();
        services.AddModuleInfo(modules);
        services.AddModulesRequests(assemblies);
        services.AddMemoryCache();
        services.AddEvents(assemblies);
        services.AddPhotos(config);
        services.AddMessaging();
        services.AddScoped<ICurrentUserContextService, CurrentUserContextService>();
        services.AddScoped<IContext, Context>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
        app.UseCors(CorsPolicy);
        app.UseErrorHandling();
        app.UseSwagger();
        app.UseSwaggerUI(swagger =>
        {
            swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "ProfesiNet API");
            swagger.RoutePrefix = string.Empty;
        });
     
        app.UseHttpsRedirection();
        // app.UseRouting(); tu jest jakis problem wywala apke
        
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
