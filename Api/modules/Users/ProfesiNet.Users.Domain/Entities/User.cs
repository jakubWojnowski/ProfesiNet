namespace ProfesiNet.Users.Domain.Entities;

public class User
{
    public Guid Id { get; init; }
    public string? Email { get; set; }
    public string? EncodedPassword { get; set; }
    public Guid? ProfileId { get; set; }
    public virtual Profile? Profile { get; set; }
}