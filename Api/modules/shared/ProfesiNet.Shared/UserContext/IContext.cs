﻿namespace ProfesiNet.Shared.UserContext;

public interface IContext
{
    Guid Id { get; }
    string FullName { get; }
    string Token { get; }
}