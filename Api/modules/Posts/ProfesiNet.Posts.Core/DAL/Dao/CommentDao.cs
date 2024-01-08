namespace ProfesiNet.Posts.Core.DAL.Dao;

public class CommentDao
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public Guid PostId { get; set; }
    public string? CreatorName { get; set; }
    public string? CreatorSurname { get; set; }
    public string? CreatorProfilePicture { get; set; }
    public string? Content { get; set; }
    public DateTime? PublishedAt { get; set; }
}

// //   Id = c.Id,
// Content = c.Content,
// PostId = postId,
// PublishedAt = c.PublishedAt,
// CreatorId = c.CreatorId,
// CreatorName = creators[c.CreatorId].Name,
// CreatorSurname = creators[c.CreatorId].Surname,