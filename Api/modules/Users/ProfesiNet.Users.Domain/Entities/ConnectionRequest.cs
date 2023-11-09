namespace ProfesiNet.Users.Domain.Entities;

public class ConnectionRequest
{
    public Guid ProfileId { get; set; }
    public Guid SenderId { get; set; }
    public virtual Profile Profile { get; set; }
    public virtual Profile Sender { get; set; }
    public DateTime RequestDate { get; set; }
}