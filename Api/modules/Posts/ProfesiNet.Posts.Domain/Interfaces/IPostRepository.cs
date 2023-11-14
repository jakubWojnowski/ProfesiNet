using System.Linq.Expressions;
using ProfesiNet.Posts.Domain.Entities;

namespace ProfesiNet.Posts.Domain.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IQueryable<Post>> GetAllAsync(CancellationToken ct, Expression<Func<Post, object>>? include = null);
    Task<Guid> AddAsync(Post entity, CancellationToken ct);
    Task UpdateAsync(Post entity, CancellationToken ct);
    Task DeleteAsync(Post entity,  CancellationToken ct);
    Task<Post?> GetRecordByFilterAsync(Expression<Func<Post, bool>> filter, CancellationToken ct);
    Task<IEnumerable<Post?>> GetAllForConditionAsync(Expression<Func<Post, bool>> filter, CancellationToken ct);
    Task<bool> AnyAsync(Expression<Func<Post, bool>> predicate, CancellationToken ct);
}