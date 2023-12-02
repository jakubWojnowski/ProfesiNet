using ProfesiNet.Posts.Core.Entities;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Persistence;

namespace ProfesiNet.Posts.Core.Repositories;

internal class ShareRepository : GenericRepository<Share,Guid>, IShareRepository
{
    public ShareRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
    }
}