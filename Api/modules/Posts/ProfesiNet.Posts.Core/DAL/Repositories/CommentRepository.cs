using Microsoft.EntityFrameworkCore;
using ProfesiNet.Posts.Core.DAL.Dao;
using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.DAL.Persistence;
using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Core.DAL.Repositories;

internal class CommentRepository : GenericRepository<Comment, Guid>, ICommentRepository
{
    private readonly ProfesiNetPostDbContext _dbContext;

    public CommentRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IQueryable<CommentDao>?> GetCommentsWithCreatorsPerPost( Guid postId,CancellationToken ct = default)
    {
        var comments = await _dbContext.Comments
            .Where(c => c.PostId == postId)
            .ToListAsync(cancellationToken: ct);

        var creatorIds = comments.Select(c => c.CreatorId).Distinct();
        var creators = await _dbContext.Creators
            .Where(c => creatorIds.Contains(c.Id))
            .ToDictionaryAsync(c => c.Id, c => new { c.Name, c.Surname, c.ProfilePicture }, cancellationToken: ct);

        var commentDaoes = comments.Select(c => new CommentDao
        {
            Id = c.Id,
            Content = c.Content,
            PostId = postId,
            PublishedAt = c.PublishedAt,
            CreatorId = c.CreatorId,
            CreatorName = creators[c.CreatorId].Name,
            CreatorSurname = creators[c.CreatorId].Surname,
            CreatorProfilePicture = creators[c.CreatorId].ProfilePicture
           
        }).OrderByDescending(c => c.PublishedAt);
        
        return commentDaoes.AsQueryable();
    }
    
    public async Task<CommentDao> GetCommentWithCreator(Guid commentId, CancellationToken ct = default)
    {
        var comment = await _dbContext.Comments
            .Where(c => c.Id == commentId)
            .FirstOrDefaultAsync(cancellationToken: ct);

        var creator = await _dbContext.Creators
            .Where(c => c.Id == comment.CreatorId)
            .FirstOrDefaultAsync(cancellationToken: ct);

        var commentDao = new CommentDao
        {
            Id = comment.Id,
            Content = comment.Content,
            PostId = comment.PostId,
            PublishedAt = comment.PublishedAt,
            CreatorId = comment.CreatorId,
            CreatorName = creator.Name,
            CreatorSurname = creator.Surname,
        };
        
        return commentDao;
    }
}