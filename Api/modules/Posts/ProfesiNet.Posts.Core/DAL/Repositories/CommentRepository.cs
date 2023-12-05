using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.DAL.Persistence;
using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Core.DAL.Repositories;

public class CommentRepository : GenericRepository<Comment, Guid>, ICommentRepository
{
    public CommentRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
    }
}