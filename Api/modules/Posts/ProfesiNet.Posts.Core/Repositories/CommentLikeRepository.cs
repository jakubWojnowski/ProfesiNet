using ProfesiNet.Posts.Core.Entities;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Persistence;

namespace ProfesiNet.Posts.Core.Repositories;

public class CommentLikeRepository : GenericRepository<CommentLike, Guid>, ICommentLikeRepository
{
    public CommentLikeRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
    }
}