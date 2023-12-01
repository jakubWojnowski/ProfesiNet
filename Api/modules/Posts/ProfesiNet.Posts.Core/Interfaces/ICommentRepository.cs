using System.Linq.Expressions;
using ProfesiNet.Posts.Core.Entities;

namespace ProfesiNet.Posts.Core.Interfaces;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<Comment>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(Comment entity, CancellationToken ct = default);
    Task UpdateAsync(Comment entity, CancellationToken ct = default);
    Task DeleteAsync(Comment entity, CancellationToken ct = default);

    Task<Comment?> GetRecordByFilterAsync(Expression<Func<Comment, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<Comment?>> GetAllForConditionAsync(Expression<Func<Comment, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<Comment, bool>> predicate, CancellationToken ct = default);
}