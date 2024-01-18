namespace ProfesiNet.Shared.Messaging
{
    public interface IMessageBroker
    {
        Task PublishMessageAsync(params IMessage[]? messages);
    }
}