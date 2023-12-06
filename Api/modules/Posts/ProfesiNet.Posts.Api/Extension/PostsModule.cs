using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Modules;

namespace ProfesiNet.Posts.Api.Extension;

public class PostsModule : IModule
{
    public const string BasePath = "posts-module";

    public string Name { get; } = "Posts";
    public string Path => BasePath;
    public void Register(IServiceCollection services)
    {
        throw new NotImplementedException();
    }

    public void Use(IApplicationBuilder app)
    {
        throw new NotImplementedException();
    }
}