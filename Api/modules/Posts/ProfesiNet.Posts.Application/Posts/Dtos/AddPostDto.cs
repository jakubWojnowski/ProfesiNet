namespace ProfesiNet.Posts.Application.Posts.Dtos;

public class AddPostDto
{
    public Guid ProfileId { get; set; }
    public string? Media { get; set; }
    public string? Description { get; set; }
}