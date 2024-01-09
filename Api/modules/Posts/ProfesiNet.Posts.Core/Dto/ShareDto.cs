namespace ProfesiNet.Posts.Core.Dto;

internal class ShareDto
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public Guid PostId { get; set; }
}

internal class ShareDetailsDto : ShareDto
{
    public PostDto Post { get; set; }
}