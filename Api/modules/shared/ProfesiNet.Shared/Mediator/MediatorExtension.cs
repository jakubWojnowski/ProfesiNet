﻿using MediatR;
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
        var handlerAssemblies = allAssemblies.Where(assembly =>
        {
            try
            {
                return assembly.GetTypes().Any(type =>
                    type.GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        (i.GetGenericTypeDefinition() == typeof(IRequestHandler<>) ||
                         i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))));
            }
            catch
            {
                return false;
            }
        });
         services.AddMediatR(cfg =>
            {
                foreach (var assembly in handlerAssemblies)
                cfg.RegisterServicesFromAssembly(assembly);
                
            });
        

        return services;
    }
}