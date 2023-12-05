using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.DAL.Persistence;
using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Core.DAL.Repositories;

internal class ShareRepository : GenericRepository<Share,Guid>, IShareRepository
{
    public ShareRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
    }
}