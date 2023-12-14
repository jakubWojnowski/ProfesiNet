using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.Repositories;

internal class UserRepository : GenericRepository<User, Guid>, IUserRepository
{
    private readonly ProfesiNetUserDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(ProfesiNetUserDbContext dbContext, ILogger<UserRepository> logger) : base(dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task UpdateFollowingsAsync(Guid userId, Guid targetUserId, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }

            var targetUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == targetUserId, cancellationToken);
            if (targetUser is null)
            {
                throw new UserNotFoundException(targetUserId);
            }

            if (user.Followings.Contains(targetUser.Id))
            {
                throw new UserAlreadyFollowedException(userId, targetUserId);
            }

            user.Followings.Add(targetUser.Id);
            targetUser.Followers.Add(user.Id);

            _dbContext.Users.Update(user);
            _dbContext.Users.Update(targetUser);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task UpdateConnectionInvitationsAsync(Guid userId, Guid targetUserId,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }

            var targetUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == targetUserId, cancellationToken);
            if (targetUser is null)
            {
                throw new UserNotFoundException(targetUserId);
            }

            if (user.NetworkConnectionInvitationsSent.Contains(targetUser.Id))
            {
                throw new UserAlreadySentInvitationException(userId, targetUserId);
            }


            targetUser.NetworkConnectionInvitationsReceived.Add(user.Id);
            user.NetworkConnectionInvitationsSent.Add(targetUser.Id);

            _dbContext.Users.Update(user);
            _dbContext.Users.Update(targetUser);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task UpdateConnectionAsync(Guid userId, Guid targetUserId, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }

            var targetUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == targetUserId, cancellationToken);
            if (targetUser is null)
            {
                throw new UserNotFoundException(targetUserId);
            }

            if (user.NetworkConnections.Contains(targetUser.Id))
            {
                throw new UserAlreadyAcceptedConnectionException(userId, targetUserId);
            }

            if (!user.NetworkConnectionInvitationsReceived.Contains(targetUser.Id))
            {
                throw new UserDoesNotHaveInvitationException(userId, targetUserId);
            }


            user.NetworkConnections.Add(targetUser.Id);
            targetUser.NetworkConnections.Add(user.Id);


            _dbContext.Users.Update(user);
            _dbContext.Users.Update(targetUser);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task DeleteConnectionAsync(Guid userId, Guid targetUserId, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }

            var targetUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == targetUserId, cancellationToken);
            if (targetUser is null)
            {
                throw new UserNotFoundException(targetUserId);
            }

            if (!user.NetworkConnections.Contains(targetUserId))
            {
                throw new UserDosesNotHaveConnectionException(userId, targetUserId);
            }

            user.NetworkConnections.Remove(targetUser.Id);
            targetUser.NetworkConnections.Remove(user.Id);

            _dbContext.Users.Update(user);
            _dbContext.Users.Update(targetUser);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task DeleteConnectionInvitationReceivedAsync(Guid userId, Guid targetUserId,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }


            if (!user.NetworkConnectionInvitationsReceived.Contains(targetUserId))
            {
                throw new UserDoesNotHaveInvitationException(userId, targetUserId);
            }

            user.NetworkConnectionInvitationsReceived.Remove(targetUserId);

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
    
    public async Task DeleteConnectionInvitationSentAsync(Guid userId, Guid targetUserId,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }
            
            var targetUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == targetUserId, cancellationToken);
            if (targetUser is null)
            {
                throw new UserNotFoundException(targetUserId);
            }
            
            if (!user.NetworkConnectionInvitationsSent.Contains(targetUserId))
            {
                throw new UserDoesNotHaveInvitationException(userId, targetUserId);
            }
            
            user.NetworkConnectionInvitationsSent.Remove(targetUserId);
            targetUser.NetworkConnectionInvitationsReceived.Remove(userId);
            
            _dbContext.Users.Update(user);
            _dbContext.Users.Update(targetUser);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task DeleteFollowingAsync(Guid userId, Guid targetUserId, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }

            var targetUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == targetUserId, cancellationToken);
            if (targetUser is null)
            {
                throw new UserNotFoundException(targetUserId);
            }

            if (!user.Followers.Contains(targetUserId))
            {
                throw new UserDosesNotHaveFollowingsException(userId, targetUserId);
            }

            user.Followings.Remove(targetUser.Id);
            targetUser.Followers.Remove(user.Id);

            _dbContext.Users.Update(user);
            _dbContext.Users.Update(targetUser);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
}