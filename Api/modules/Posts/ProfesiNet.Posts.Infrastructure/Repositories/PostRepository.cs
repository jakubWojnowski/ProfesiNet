using ProfesiNet.Posts.Domain.Entities;
using ProfesiNet.Posts.Domain.Interfaces;
using ProfesiNet.Posts.Infrastructure.Persistence;

namespace ProfesiNet.Posts.Infrastructure.Repositories;

public class PostRepository : GenericRepository<Post, Guid>, IPostRepository
{
    public PostRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
        
    }
    
}