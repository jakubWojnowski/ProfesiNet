using System.Linq.Expressions;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Domain.Interfaces;

public interface ICertificateRepository
{
    Task<Certificate?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<Certificate>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(Certificate entity, CancellationToken ct = default);
    Task UpdateAsync(Certificate entity, CancellationToken ct = default);
    Task DeleteAsync(Certificate entity, CancellationToken ct = default);

    Task<Certificate?> GetRecordByFilterAsync(Expression<Func<Certificate, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<Certificate?>> GetAllForConditionAsync(Expression<Func<Certificate, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<Certificate, bool>> predicate, CancellationToken ct = default);
}