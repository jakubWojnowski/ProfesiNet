namespace ProfesiNet.Users.Domain.Entities;

public class Skill
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}