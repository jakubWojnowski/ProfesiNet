namespace ProfesiNet.Posts.Core.Dto;

internal class CommentLikeDto
{
    public Guid Id { get; set; }
}

internal class CommentLikeDetailsDto : CommentLikeDto
{
    public Guid CreatorId { get; set; }
    public Guid CommentId { get; set; }
}