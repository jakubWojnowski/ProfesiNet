﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ProfesiNet.Shared.Commands
{
    internal static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>))
                    .WithoutAttribute<DecoratorAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}