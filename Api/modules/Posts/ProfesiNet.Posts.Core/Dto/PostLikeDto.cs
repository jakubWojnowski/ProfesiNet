namespace ProfesiNet.Posts.Core.Dto;

internal class PostLikeDto
{ 
    public Guid Id { get; set; }
}

internal class PostLikeDetailsDto : PostLikeDto
{
    public Guid CreatorId { get; set; }
    public Guid PostId { get; set; }
}