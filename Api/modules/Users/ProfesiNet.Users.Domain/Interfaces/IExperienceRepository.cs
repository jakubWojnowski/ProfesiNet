using System.Linq.Expressions;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Domain.Interfaces;

public interface IExperienceRepository
{
    Task<Experience?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<Experience>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(Experience entity, CancellationToken ct = default);
    Task UpdateAsync(Experience entity, CancellationToken ct = default);
    Task DeleteAsync(Experience entity, CancellationToken ct = default);

    Task<Experience?> GetRecordByFilterAsync(Expression<Func<Experience, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<Experience?>> GetAllForConditionAsync(Expression<Func<Experience, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<Experience, bool>> predicate, CancellationToken ct = default);
}