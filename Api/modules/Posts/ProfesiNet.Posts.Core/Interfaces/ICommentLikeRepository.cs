using System.Linq.Expressions;
using ProfesiNet.Posts.Core.DAL.Entities;

namespace ProfesiNet.Posts.Core.Interfaces;

public interface ICommentLikeRepository
{
    Task<CommentLike?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<CommentLike>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(CommentLike entity, CancellationToken ct = default);
    Task UpdateAsync(CommentLike entity, CancellationToken ct = default);
    Task DeleteAsync(CommentLike entity, CancellationToken ct = default);

    Task<CommentLike?> GetRecordByFilterAsync(Expression<Func<CommentLike, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<CommentLike?>> GetAllForConditionAsync(Expression<Func<CommentLike, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<CommentLike, bool>> predicate, CancellationToken ct = default);
}