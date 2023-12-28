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

//
// public static IServiceCollection AddProfesiNetMediator(this IServiceCollection services)
// {
//     var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
//     var handlerAssemblies = allAssemblies.Where(assembly =>
//     {
//         try
//         {
//             return assembly.GetTypes().Any(type =>
//                 type.GetInterfaces().Any(i =>
//                     (i.IsGenericType &&
//                      (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
//                       i.GetGenericTypeDefinition() == typeof(INotificationHandler<>))) &&
//                     !type.IsDefined(typeof(DecoratorAttribute), false)));
//         }
//         catch
//         {
//             // In case the assembly cannot be scanned for types, for example, if it's dynamic.
//             return false;
//         }
//     });
//
//     services.Scan(scan => scan
//         .FromAssemblies(handlerAssemblies)
//         .AddClasses(classes => classes.AssignableToAny(
//                 typeof(IRequestHandler<,>),
//                 typeof(INotificationHandler<>))
//             .WithoutAttribute<DecoratorAttribute>())
//         .AsImplementedInterfaces()
//         .WithScopedLifetime());
//
//     return services;
// }
