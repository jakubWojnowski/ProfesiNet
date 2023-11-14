using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProfesiNet.Posts.Infrastructure.Persistence;

namespace ProfesiNet.Posts.Infrastructure.Repositories;

public class GenericRepository<TEntity, TKey>  where TEntity : class
{
    private readonly ProfesiNetPostDbContext _dbContext;
    private readonly DbSet<TEntity> _entities;

    public GenericRepository(ProfesiNetPostDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct) => await _dbContext.Set<TEntity>().FindAsync(new object?[] { id, ct }, cancellationToken: ct);

    public async Task<IQueryable<TEntity>> GetAllAsync(CancellationToken ct, Expression<Func<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();

        if (include != null)
        {
           query = query.Include(include);
        }

        var result = await query.ToListAsync(ct);
        return result.AsQueryable();

    }
    public async Task<Guid> AddAsync(TEntity entity, CancellationToken ct)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);
        var property = _dbContext.Entry(entity).Property("Id");
        return (Guid)(property.CurrentValue ?? throw new InvalidOperationException());
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken ct)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(TEntity entity,  CancellationToken ct)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync(ct);
    }
    public async Task<TEntity?> GetRecordByFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct)
    {
        return await _entities.Where(filter).FirstOrDefaultAsync(ct);
    }
    public async Task<IEnumerable<TEntity?>> GetAllForConditionAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct)
    {
        return await _entities.Where(filter).ToListAsync(ct);
    }
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(predicate, ct);
    }
}