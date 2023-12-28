using ProfesiNet.Shared.MsSql;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.DAL;

internal class UsersUnitOfWork : MsSqlUnitOfWork<ProfesiNetUserDbContext>, IUsersUnitOfWork
{
    public UsersUnitOfWork(ProfesiNetUserDbContext dbContext) : base(dbContext)
    {
    }
}