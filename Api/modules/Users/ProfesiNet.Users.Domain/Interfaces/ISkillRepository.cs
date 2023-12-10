using System.Linq.Expressions;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Domain.Interfaces;

public interface ISkillRepository
{
    Task<Skill?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<Skill>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(Skill entity, CancellationToken ct = default);
    Task UpdateAsync(Skill entity, CancellationToken ct = default);
    Task DeleteAsync(Skill entity, CancellationToken ct = default);

    Task<Skill?> GetRecordByFilterAsync(Expression<Func<Skill, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<Skill?>> GetAllForConditionAsync(Expression<Func<Skill, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<Skill, bool>> predicate, CancellationToken ct = default);
}