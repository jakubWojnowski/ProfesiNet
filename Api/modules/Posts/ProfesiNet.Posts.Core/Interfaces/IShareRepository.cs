using System.Linq.Expressions;
using ProfesiNet.Posts.Core.DAL.Entities;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface IShareRepository
{
    Task<Share?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<Share>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(Share entity, CancellationToken ct = default);
    Task UpdateAsync(Share entity, CancellationToken ct = default);
    Task DeleteAsync(Share entity, CancellationToken ct = default);

    Task<Share?> GetRecordByFilterAsync(Expression<Func<Share, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<Share?>> GetAllForConditionAsync(Expression<Func<Share, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<Share, bool>> predicate, CancellationToken ct = default);
}