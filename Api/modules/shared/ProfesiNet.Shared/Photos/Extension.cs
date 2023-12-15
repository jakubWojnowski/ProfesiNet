using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Configurations;

namespace ProfesiNet.Shared.Photos;

internal static class Extension
{
    public static IServiceCollection AddPhotos(this IServiceCollection services, IConfiguration configuration)
    {
        // var options = services.GetOptions<CloudinarySettings>("Cloudinary");
        // services.AddSingleton(options);
        services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));

        services.AddScoped<IPhotoAccessor, PhotoAccessor>();
       
        return services;
    }
}