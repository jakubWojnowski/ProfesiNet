using System.Linq.Expressions;
using ProfesiNet.Posts.Core.DAL.Dao;
using ProfesiNet.Posts.Core.DAL.Entities;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface ICommentRepository
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
    Task<IQueryable<CommentDao>?> GetCommentsWithCreatorsPerPost( Guid postId,CancellationToken ct = default);
    Task<CommentDao> GetCommentWithCreator(Guid commentId, CancellationToken ct = default);
}