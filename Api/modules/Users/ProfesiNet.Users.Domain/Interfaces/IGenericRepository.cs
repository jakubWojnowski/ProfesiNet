using System.Linq.Expressions;

namespace ProfesiNet.Users.Domain.Interfaces;

public interface IGenericRepository<TEntity, TKey> where TEntity : class
{
    public Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
    public Task<IQueryable<TEntity>> GetAllAsync(CancellationToken ct = default);
    public Task<Guid> AddAsync(TEntity entity, CancellationToken ct = default);
    public Task UpdateAsync(TEntity entity, CancellationToken ct = default);
    public Task DeleteAsync(TEntity entity, CancellationToken ct = default);

    public Task<TEntity?> GetRecordByFilterAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken ct = default);

    public Task<IEnumerable<TEntity?>> GetAllForConditionAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken ct = default);

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
}