namespace ProfesiNet.Shared.MsSql;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default);
}