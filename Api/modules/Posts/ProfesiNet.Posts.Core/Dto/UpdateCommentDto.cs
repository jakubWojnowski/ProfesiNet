namespace ProfesiNet.Posts.Core.Dto;

public class UpdateCommentDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public DateTime? PublishedAt { get; set; }
}