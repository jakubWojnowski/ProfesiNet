using System.Linq.Expressions;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Domain.Interfaces;

public interface INetworkConnectionRepository
{

    Task AddAsync(NetworkConnection connection, CancellationToken ct = default);
    Task DeleteAsync(NetworkConnection connection, CancellationToken ct = default);
    Task<NetworkConnection?> GetForConditionAsync(Expression<Func<NetworkConnection, bool>> filter, CancellationToken ct = default);
    Task<IEnumerable<NetworkConnection?>> GetAllForConditionAsync(Expression<Func<NetworkConnection, bool>> filter, CancellationToken ct = default);
    
}