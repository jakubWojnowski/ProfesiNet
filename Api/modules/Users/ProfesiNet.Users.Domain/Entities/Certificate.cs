namespace ProfesiNet.Users.Domain.Entities;

public class Certificate
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateOnly Date { get; set; }
    
    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }
}