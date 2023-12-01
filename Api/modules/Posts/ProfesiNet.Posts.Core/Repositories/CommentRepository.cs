using ProfesiNet.Posts.Core.Entities;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Persistence;

namespace ProfesiNet.Posts.Core.Repositories;

public class CommentRepository : GenericRepository<Comment, Guid>, ICommentRepository
{
    public CommentRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
    }
}