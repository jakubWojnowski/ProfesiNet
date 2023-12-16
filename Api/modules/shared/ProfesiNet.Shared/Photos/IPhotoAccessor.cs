using Microsoft.AspNetCore.Http;

namespace ProfesiNet.Shared.Photos;

public interface IPhotoAccessor
{
    Task<PhotoUploadResult?> AddPhoto(IFormFile file);
    Task<string?> DeletePhoto(string publicId);
}