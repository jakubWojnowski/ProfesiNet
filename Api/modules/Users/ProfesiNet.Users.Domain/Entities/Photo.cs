using ProfesiNet.Users.Domain.Enums;

namespace ProfesiNet.Users.Domain.Entities;

public class Photo
{
    public Guid  Id { get; set; }
    public string? Url { get; set; }
    public string? PublicId { get; set; }
    public PictureType PictureType { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    
}
