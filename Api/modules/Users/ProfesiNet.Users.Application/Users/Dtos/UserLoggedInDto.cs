namespace ProfesiNet.Users.Application.Users.Dtos;

internal class UserLoggedInDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Token { get; set; }
    public string? Address { get; set; }
    public string? Bio { get; set; }
    public string? ProfilePhoto{ get; set; }
}