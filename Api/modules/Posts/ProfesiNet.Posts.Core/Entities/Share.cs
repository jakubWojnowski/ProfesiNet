namespace ProfesiNet.Posts.Core.Entities;

internal class Share
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public Guid PostId { get; set; }
   
    
    public virtual Post Post { get; set; }
}