namespace ProfesiNet.Users.Domain.Entities;

public class Connection
{
    public Guid ProfileId { get; set; }
    public Guid FriendId { get; set; }
    public Profile Profile { get; set; }
    public Profile Friend { get; set; }
    
}