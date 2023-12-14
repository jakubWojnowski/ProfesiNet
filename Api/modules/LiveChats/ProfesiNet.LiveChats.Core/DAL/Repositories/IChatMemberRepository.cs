using System.Linq.Expressions;
using ProfesiNet.LiveChats.Core.DAL.Entities;

namespace ProfesiNet.LiveChats.Core.DAL.Repositories;

internal interface IChatMemberRepository
{
    Task<ChatMember?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IQueryable<ChatMember>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(ChatMember entity, CancellationToken ct = default);
    Task UpdateAsync(ChatMember entity, CancellationToken ct = default);
    Task DeleteAsync(ChatMember entity, CancellationToken ct = default);

    Task<ChatMember?> GetRecordByFilterAsync(Expression<Func<ChatMember, bool>> filter,
        CancellationToken ct = default);

    Task<IEnumerable<ChatMember?>> GetAllForConditionAsync(Expression<Func<ChatMember, bool>> filter,
        CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<ChatMember, bool>> predicate, CancellationToken ct = default);
}