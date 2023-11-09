namespace ProfesiNet.Users.Domain.Entities;

public class Connection
{
    public Guid ProfileId { get; set; }
    public Guid FriendId { get; set; }
    public virtual Profile Profile { get; set; }
    public virtual Profile Friend { get; set; }
    
}