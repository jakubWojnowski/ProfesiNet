using System.Threading.Channels;

namespace ProfesiNet.Shared.Messaging.Dispatcher;

internal sealed class MessageChannel : IMessageChannel
{
    private readonly Channel<IMessage> _channel = Channel.CreateUnbounded<IMessage>();
    public ChannelReader<IMessage> Reader => _channel.Reader;
    public ChannelWriter<IMessage> Writer => _channel.Writer;
}