using ProfesiNet.Shared.Modules;

namespace ProfesiNet.Shared.Messaging.Brokers;

internal class InMemoryMessageBroker : IMessageBroker
{
    private readonly IModuleClient _moduleClient;

    public InMemoryMessageBroker(IModuleClient moduleClient)
    {
        _moduleClient = moduleClient;
    }

    public async Task PublishAsync(params IMessage[] messages)
    {
        if (messages is null)
        {
            return;
        }
        messages = messages.Where(m => m is not null).ToArray();
        if (!messages.Any())
        {
            return;
        }

        var tasks = new List<Task>();
        foreach (var message in messages)
        {
            tasks.Add(_moduleClient.PublishAsync(message));
        }
        await Task.WhenAll(tasks);
    }
    
}