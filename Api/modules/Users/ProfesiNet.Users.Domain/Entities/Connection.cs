namespace ProfesiNet.Users.Domain.Entities;

public class Connection
{
    public Guid ProfileId { get; init; }
    public Guid FriendId { get; set; }
    public virtual User Profile { get; set; }
    public virtual User Friend { get; set; }
    
}