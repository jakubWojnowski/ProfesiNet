namespace ProfesiNet.Users.Application.Users.Services.UserContect;

public class CurrentUserContext
{
    public CurrentUserContext(string? fullname, string? id)
    {
        FullName = fullname;
        Id = id;
    }
    public string? Id { get; set; }
    public string? FullName { get; set; }
}