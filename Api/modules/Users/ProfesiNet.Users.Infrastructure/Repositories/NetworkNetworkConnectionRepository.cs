using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.Repositories;

internal class NetworkNetworkConnectionRepository  : INetworkConnectionRepository
{
    private readonly ProfesiNetUserDbContext _dbContext;

    public NetworkNetworkConnectionRepository(ProfesiNetUserDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task AddAsync(NetworkConnection connection, CancellationToken ct = default)
    {
        await _dbContext.NetworkConnections.AddAsync(connection, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(NetworkConnection connection, CancellationToken ct = default)
    {
        _dbContext.NetworkConnections.Remove(connection);
        await _dbContext.SaveChangesAsync(ct);
    }
    

    public async Task<NetworkConnection?> GetForConditionAsync(Expression<Func<NetworkConnection, bool>> filter, CancellationToken ct = default)
    {
        return await _dbContext.NetworkConnections.Where(filter).FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<NetworkConnection?>> GetAllForConditionAsync(Expression<Func<NetworkConnection, bool>> filter, CancellationToken ct = default)
    {
        return await _dbContext.NetworkConnections.Where(filter).ToListAsync(ct);
    }
}