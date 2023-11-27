namespace ProfesiNet.Users.Domain.Entities;

public class ConnectionRequest
{
    public Guid ReceiverId { get; init; }
    public Guid SenderId { get; set; }
    public virtual User Receiver { get; set; }
    public virtual User Sender { get; set; }
    public DateOnly RequestDate { get; set; }
}