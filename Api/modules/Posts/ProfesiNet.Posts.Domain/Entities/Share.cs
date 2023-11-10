namespace ProfesiNet.Posts.Domain.Entities;

public class Share
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public Guid PostId { get; set; }
    public DateTime SharedAt { get; set; }
    
    public virtual Post Post { get; set; }
}