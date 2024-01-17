namespace ProfesiNet.Shared.Messaging.Dispatcher;

internal interface IAsyncMessageDispatcher
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
}