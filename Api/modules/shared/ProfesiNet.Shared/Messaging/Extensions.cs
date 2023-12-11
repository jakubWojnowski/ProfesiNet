using Confab.Shared.Infrastructure.Messaging.Dispatcher;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Configurations;
using ProfesiNet.Shared.Messaging.Brokers;
using ProfesiNet.Shared.Messaging.Dispatcher;

namespace ProfesiNet.Shared.Messaging;

internal static class Extensions
{
    private const string SectionName = "messaging";

    internal static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageMessageDispatcher>();
        

        var messagingOptions = services.GetOptions<MessagingOptions>(SectionName);
        services.AddSingleton(messagingOptions);
        if (messagingOptions.UseBackgroundDispatcher)
        {
            services.AddHostedService<BackGroundDispatcher>();
            
        }
        return services;
    }
}