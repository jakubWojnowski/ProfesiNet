namespace ProfesiNet.Posts.Core.Dto;

public class ShareDto
{
    public Guid Id { get; set; }

}

public class ShareDetailsDto
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public Guid PostId { get; set; }
    
}