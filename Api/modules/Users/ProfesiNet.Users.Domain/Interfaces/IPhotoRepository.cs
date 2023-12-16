using System.Linq.Expressions;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Domain.Interfaces;

public interface IPhotoRepository
{
    Task<Photo?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<Photo>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(Photo entity, CancellationToken ct = default);
    Task UpdateAsync(Photo entity, CancellationToken ct = default);
    Task DeleteAsync(Photo entity, CancellationToken ct = default);

    Task<Photo?> GetRecordByFilterAsync(Expression<Func<Photo, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<Photo?>> GetAllForConditionAsync(Expression<Func<Photo, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<Photo, bool>> predicate, CancellationToken ct = default);
}