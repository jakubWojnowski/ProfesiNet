using System.Linq.Expressions;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Domain.Interfaces;

public interface IEducationRepository
{
    Task<Education?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<Education>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(Education entity, CancellationToken ct = default);
    Task UpdateAsync(Education entity, CancellationToken ct = default);
    Task DeleteAsync(Education entity, CancellationToken ct = default);

    Task<Education?> GetRecordByFilterAsync(Expression<Func<Education, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<Education?>> GetAllForConditionAsync(Expression<Func<Education, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<Education, bool>> predicate, CancellationToken ct = default);
}