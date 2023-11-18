using System.Linq.Expressions;

namespace ProfesiNet.Users.Domain.Interfaces;

public interface IGenericRepository<TEntity, TKey> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
    Task<IQueryable<TEntity>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(TEntity entity, CancellationToken ct = default);
    Task UpdateAsync(TEntity entity, CancellationToken ct = default);
    Task DeleteAsync(TEntity entity, CancellationToken ct = default);

    Task<TEntity?> GetRecordByFilterAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<TEntity?>> GetAllForConditionAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
}