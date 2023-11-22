namespace ProfesiNet.Users.Domain.Entities;

public class Following
{
    public Guid ObserverId { get; init; }
    public Guid TargetId { get; set; }
    public virtual User Observer { get; set; }
    public virtual User Target { get; set; }
}