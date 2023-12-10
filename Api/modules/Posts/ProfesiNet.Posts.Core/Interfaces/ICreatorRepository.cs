using System.Linq.Expressions;
using ProfesiNet.Posts.Core.DAL.Entities;

namespace ProfesiNet.Posts.Core.Interfaces;

public interface ICreatorRepository
{
    Task<Creator?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<Creator>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(Creator entity, CancellationToken ct = default);
    Task UpdateAsync(Creator entity, CancellationToken ct = default);
    Task DeleteAsync(Creator entity, CancellationToken ct = default);

    Task<Creator?> GetRecordByFilterAsync(Expression<Func<Creator, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<Creator?>> GetAllForConditionAsync(Expression<Func<Creator, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<Creator, bool>> predicate, CancellationToken ct = default);
}