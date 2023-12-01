namespace ProfesiNet.Posts.Core.Entities;

public class CommentLike
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public Guid CommentId { get; set; }
    

    public virtual Comment? Comment { get; set; }
}