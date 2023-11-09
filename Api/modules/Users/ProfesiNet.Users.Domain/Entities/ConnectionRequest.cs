namespace ProfesiNet.Users.Domain.Entities;

public class ConnectionRequest
{
    public Guid ProfileId { get; set; }
    public Guid SenderId { get; set; }
    public Profile Profile { get; set; }
    public Profile Sender { get; set; }
    public DateTime RequestDate { get; set; }
}