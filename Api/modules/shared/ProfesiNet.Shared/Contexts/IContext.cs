namespace ProfesiNet.Shared.Contexts;

public interface IContext
{
    Guid Id { get; }
    string FullName { get; }
    string Token { get; }
}