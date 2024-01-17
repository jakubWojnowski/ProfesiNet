using ProfesiNet.Shared.Messaging.Dispatcher;
using ProfesiNet.Shared.Modules;

namespace ProfesiNet.Shared.Messaging.Brokers;

internal class MessageBroker : IMessageBroker
{
    private readonly IModuleClient _moduleClient;
    private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;
    private readonly MessagingOptions _messagingOptions;

    public MessageBroker(IModuleClient moduleClient, IAsyncMessageDispatcher asyncMessageDispatcher, MessagingOptions messagingOptions)
    {
        _moduleClient = moduleClient;
        _asyncMessageDispatcher = asyncMessageDispatcher;
        _messagingOptions = messagingOptions;
    }

    public async Task PublishAsync(params IMessage[]? messages)
    {
        if (messages is null)
        {
            return;
        }
        messages = messages.Where(m => true).ToArray();
        if (!messages.Any())
        {
            return;
        }

        var tasks = new List<Task>();
        foreach (var message in messages)
        {
            if (_messagingOptions.UseBackgroundDispatcher)
            {
                await _asyncMessageDispatcher.PublishAsync(message);
                continue;
            }
            tasks.Add(_moduleClient.PublishAsync(message));
        }
        await Task.WhenAll(tasks);
    }
    
}