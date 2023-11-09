namespace ProfesiNet.Users.Domain.Entities;

public class Following
{
    public Guid ObserverId { get; set; }
    public Guid TargetId { get; set; }
    public virtual Profile Observer { get; set; }
    public virtual Profile Target { get; set; }
}