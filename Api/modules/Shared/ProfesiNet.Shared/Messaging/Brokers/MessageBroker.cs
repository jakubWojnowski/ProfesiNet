using ProfesiNet.Shared.Messaging.Dispatcher;
using ProfesiNet.Shared.Modules;

namespace ProfesiNet.Shared.Messaging.Brokers;

internal class MessageBroker : IMessageBroker
{
    private readonly IModuleClient _client;
    private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;
    private readonly MessagingOptions _messagingOptions;

    public MessageBroker(IModuleClient client, IAsyncMessageDispatcher asyncMessageDispatcher, MessagingOptions messagingOptions)
    {
        _client = client;
        _asyncMessageDispatcher = asyncMessageDispatcher;
        _messagingOptions = messagingOptions;
    }

    public async Task PublishMessageAsync(params IMessage[]? messages)
    {
        if (messages is null) return;
        messages = messages.Where(msg => true).ToArray();
        if (!messages.Any()) return;
        var tasks = new List<Task>();
        foreach (var message in messages)
        {
            if (_messagingOptions.UseBackgroundDispatcher)
            {
                await _asyncMessageDispatcher.PublishAsync(message);
                continue;
            }
            tasks.Add(_client.PublishAsync(message));
        }
        await Task.WhenAll(tasks);
    }
    
}