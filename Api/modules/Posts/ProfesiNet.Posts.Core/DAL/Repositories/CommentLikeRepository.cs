using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.DAL.Persistence;
using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Core.DAL.Repositories;

public class CommentLikeRepository : GenericRepository<CommentLike, Guid>, ICommentLikeRepository
{
    public CommentLikeRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
    }
}