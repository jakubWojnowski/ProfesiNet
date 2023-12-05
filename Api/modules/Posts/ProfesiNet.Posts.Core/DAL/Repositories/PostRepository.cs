using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.DAL.Persistence;
using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Core.DAL.Repositories;

internal class PostRepository : GenericRepository<Post, Guid>, IPostRepository
{
    public PostRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
        
    }
    
}