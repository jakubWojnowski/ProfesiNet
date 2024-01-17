﻿namespace ProfesiNet.Shared.Modules;

internal sealed class ModuleClient : IModuleClient
{
    private readonly IModuleSerializer _moduleSerializer;
    private readonly IModuleRegistry _moduleRegistry;

    public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer)
    {
        _moduleSerializer = moduleSerializer;
        _moduleRegistry = moduleRegistry;
    }


    public async Task PublishAsync(object message)
    {
        var key = message.GetType().Name;
        var registrations = _moduleRegistry
            .GetBroadcastRegistrations(key)
            .Where(r => r.ReceiverType != message.GetType());

        var tasks = new List<Task>();
            
        foreach (var registration in registrations)
        {
            var action = registration.Action;
            var receiverMessage = TranslateType(message, registration.ReceiverType);
            if (receiverMessage != null) tasks.Add(action(receiverMessage));
        }

        await Task.WhenAll(tasks);
    }

    private T? TranslateType<T>(object value)
        => _moduleSerializer.Deserialize<T>(_moduleSerializer.Serialize(value));
        
    private object? TranslateType(object value, Type type)
        => _moduleSerializer.Deserialize(_moduleSerializer.Serialize(value), type);
}