using System.Linq.Expressions;
using ProfesiNet.Posts.Domain.Entities;

namespace ProfesiNet.Posts.Domain.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<Post>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(Post entity, CancellationToken ct = default);
    Task UpdateAsync(Post entity, CancellationToken ct = default);
    Task DeleteAsync(Post entity,  CancellationToken ct = default);
    Task<Post?> GetRecordByFilterAsync(Expression<Func<Post, bool>> filter, CancellationToken ct = default);
    Task<IEnumerable<Post?>> GetAllForConditionAsync(Expression<Func<Post, bool>> filter, CancellationToken ct = default);
    Task<bool> AnyAsync(Expression<Func<Post, bool>> predicate, CancellationToken ct = default);
}