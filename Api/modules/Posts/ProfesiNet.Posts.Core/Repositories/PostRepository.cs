using ProfesiNet.Posts.Core.Entities;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Persistence;

namespace ProfesiNet.Posts.Core.Repositories;

internal class PostRepository : GenericRepository<Post, Guid>, IPostRepository
{
    public PostRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
        
    }
    
}