using ProfesiNet.Users.Application.Photos.Dtos;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Photos.Mappings;

[Mapper]
internal partial class PhotoMapper
{

    public partial PhotoDto MapPhotoToPhotoDto(Photo photo);
    public partial Photo MapPhotoDtoToPhoto(PhotoDto photoDto);
}