using Microsoft.EntityFrameworkCore;

namespace ProfesiNet.Shared.MsSql;

public class MsSqlUnitOfWork<T> : IUnitOfWork where T : DbContext
{
    private readonly T _dbContext;

    public MsSqlUnitOfWork(T dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await action();
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        
    }
}