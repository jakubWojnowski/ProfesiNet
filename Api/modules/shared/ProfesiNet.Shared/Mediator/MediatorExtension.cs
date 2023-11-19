using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace ProfesiNet.Shared.Mediator;

internal static class MediatorExtension
{
    public static IServiceCollection AddProfesiNetMediator(this IServiceCollection services)
    {
        var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in allAssemblies)
        {
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(assembly); });
        }

        return services;
    }
}