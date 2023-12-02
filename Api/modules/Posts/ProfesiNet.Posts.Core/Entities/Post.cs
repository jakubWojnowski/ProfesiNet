namespace ProfesiNet.Posts.Core.Entities;

public class Post
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public string? Media { get; set; }
    public string? Description { get; set; }
    public DateTime PublishedAt { get; set; }
    
    public virtual ICollection<Share>? Shares { get; set; } = new List<Share>();
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<PostLike> Likes { get; set; } = new List<PostLike>();

}