using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Messaging.Brokers;

namespace ProfesiNet.Shared.Messaging;

internal static class Extensions
{
    internal static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
        return services;
    }
}