﻿namespace ProfesiNet.Shared.Modules;

public interface IModuleRegistry
{
    IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key); 
    void AddBroadcastAction(Type requestType, Func<object, Task> action);
}