namespace ProfesiNet.Posts.Core.Dto;

internal class PostLikeDto
{ 
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public Guid PostId { get; set; }
}

internal class PostLikeDetailsDto : PostLikeDto
{
  
}