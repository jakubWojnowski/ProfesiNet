namespace ProfesiNet.Posts.Domain.Entities;

public class PostLike
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public Guid PostId { get; set; }
    public virtual Post? Post { get; set; }
}