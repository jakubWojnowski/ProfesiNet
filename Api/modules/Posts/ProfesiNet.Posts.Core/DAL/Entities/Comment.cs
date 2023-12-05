namespace ProfesiNet.Posts.Core.DAL.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public Guid PostId { get; set; }
    public string? Content { get; set; }
    public DateTime? PublishedAt { get; set; }
    public virtual ICollection<CommentLike> Likes { get; set; } = new List<CommentLike>();
    
    public virtual Post Post { get; set; }

}