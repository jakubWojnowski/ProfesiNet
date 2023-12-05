using System.Linq.Expressions;
using ProfesiNet.Posts.Core.DAL.Entities;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface IPostLikeRepository
{
    Task<PostLike?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<PostLike>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(PostLike entity, CancellationToken ct = default);
    Task UpdateAsync(PostLike entity, CancellationToken ct = default);
    Task DeleteAsync(PostLike entity, CancellationToken ct = default);

    Task<PostLike?> GetRecordByFilterAsync(Expression<Func<PostLike, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<PostLike?>> GetAllForConditionAsync(Expression<Func<PostLike, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<PostLike, bool>> predicate, CancellationToken ct = default);
}