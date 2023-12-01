using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.Repositories;

internal abstract class GenericRepository<TEntity, TKey>  where TEntity : class
{
    private readonly ProfesiNetUserDbContext _dbContext;
    private readonly DbSet<TEntity> _entities;

    protected GenericRepository(ProfesiNetUserDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<TEntity>();
    }

    public  async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default) =>
        await _dbContext.Set<TEntity>().FindAsync(new object?[] { id, ct }, cancellationToken: ct);

    public async Task<IQueryable<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        var result = await _entities.ToListAsync(ct);
        return result.AsQueryable();
    }

    public async Task<Guid> AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);
        var property = _dbContext.Entry(entity).Property("Id");
        return (Guid)(property.CurrentValue ?? throw new InvalidOperationException());
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<TEntity?> GetRecordByFilterAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken ct = default)
    {
        return await _entities.Where(filter).FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<TEntity?>> GetAllForConditionAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken ct = default)
    {
        return await _entities.Where(filter).ToListAsync(ct);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(predicate, ct);
    }
}
//to do Dev mentorsi stwierdzili ze generyczne repo jest okay ale w przypadku crudow i moge je uzyc jako bazowe dla innych repo ale w kompozycji najlepiej.
//przemyslec czy modul users moze miec generyczne repo i czy to nie bedzie problemem w przyszlosci
//napewno modul Post bedzie mial genereyczne repo bo tam beda tylko crudy i nie bedzie problemu z kompozycja