namespace ProfesiNet.Shared.Messaging
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IMessage[]? messages);
    }
}