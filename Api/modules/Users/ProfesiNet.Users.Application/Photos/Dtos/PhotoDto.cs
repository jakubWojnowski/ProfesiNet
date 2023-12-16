using ProfesiNet.Users.Domain.Enums;

namespace ProfesiNet.Users.Application.Photos.Dtos;

internal class PhotoDto
{
    public Guid  Id { get; set; }
    public string? Url { get; set; }
    public string? PublicId { get; set; }
    public PictureType PictureType { get; set; }
    public Guid UserId { get; set; }
}