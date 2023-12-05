namespace ProfesiNet.Posts.Core.DAL.Entities;

public class PostLike
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public Guid PostId { get; set; }
   

    public virtual Post? Post { get; set; }
}