namespace ProfesiNet.Users.Domain.Entities;

public class User
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public Guid ProfileId { get; set; }
    public virtual Profile Profile { get; set; }
}