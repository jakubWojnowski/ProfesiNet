namespace ProfesiNet.Posts.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public Guid PostId { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public virtual ICollection<CommentLike> Likes { get; set; } = new List<CommentLike>();
    
    public virtual Post Post { get; set; }

}