using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User,Guid>, IUserRepository
{
    public UserRepository(ProfesiNetUserDbContext dbContext) : base(dbContext)
    {
    }
}