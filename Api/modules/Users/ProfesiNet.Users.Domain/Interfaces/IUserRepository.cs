using System.Linq.Expressions;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<User>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(User entity, CancellationToken ct = default);
    Task UpdateAsync(User entity, CancellationToken ct = default);
    Task DeleteAsync(User entity, CancellationToken ct = default);
    Task UpdateFollowingsAsync(Guid  userId, Guid targetUserId, CancellationToken cancellationToken);

    Task<User?> GetRecordByFilterAsync(Expression<Func<User, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<User?>> GetAllForConditionAsync(Expression<Func<User, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<User, bool>> predicate, CancellationToken ct = default);
}