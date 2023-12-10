using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.DAL.Persistence;
using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Core.DAL.Repositories;

public class CreatorRepository : GenericRepository<Creator, Guid>, ICreatorRepository
{
    public CreatorRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
    }
}