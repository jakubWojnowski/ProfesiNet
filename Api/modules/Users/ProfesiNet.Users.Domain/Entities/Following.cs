namespace ProfesiNet.Users.Domain.Entities;

public class Following
{
    public Guid ObserverId { get; set; }
    public Guid TargetId { get; set; }
    public Profile Observer { get; set; }
    public Profile Target { get; set; }
}