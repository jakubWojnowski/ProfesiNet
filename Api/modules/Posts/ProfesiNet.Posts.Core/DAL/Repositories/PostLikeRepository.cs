using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.DAL.Persistence;
using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Core.DAL.Repositories;

internal class PostLikeRepository : GenericRepository<PostLike,Guid>, IPostLikeRepository
{
    public PostLikeRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
    }
}