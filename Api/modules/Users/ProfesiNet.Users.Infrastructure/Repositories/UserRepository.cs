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
}