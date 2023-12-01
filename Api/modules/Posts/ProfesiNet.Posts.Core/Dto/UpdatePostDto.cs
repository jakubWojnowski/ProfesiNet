namespace ProfesiNet.Posts.Core.Dto;

internal class UpdatePostDto
{
    public Guid Id { get; set; }

    public string? Media { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } 
}