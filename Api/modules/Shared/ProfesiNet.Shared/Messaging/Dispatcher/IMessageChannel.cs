using System.Threading.Channels;

namespace ProfesiNet.Shared.Messaging.Dispatcher;

public interface IMessageChannel
{
    ChannelReader<IMessage> Reader { get; }
    ChannelWriter<IMessage> Writer { get; }
}