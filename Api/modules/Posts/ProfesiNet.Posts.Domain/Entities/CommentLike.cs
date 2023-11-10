namespace ProfesiNet.Posts.Domain.Entities;

public class CommentLike
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public Guid CommentId { get; set; }
    public virtual Comment? Comment { get; set; }
}