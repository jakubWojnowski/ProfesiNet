using Confab.Shared.Infrastructure.Messaging.Dispatcher;

namespace ProfesiNet.Shared.Messaging.Dispatcher;

internal sealed class AsyncMessageMessageDispatcher : IAsyncMessageDispatcher
{
    private readonly IMessageChannel _channel;

    public AsyncMessageMessageDispatcher(IMessageChannel channel)
    {
        _channel = channel;
    }

    public async Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage
    => await _channel.Writer.WriteAsync(message);
}