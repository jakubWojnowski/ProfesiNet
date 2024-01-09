using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Posts.Core.Extension;
using ProfesiNet.Shared.Modules;

namespace ProfesiNet.Posts.Api.Extension;

public class PostsModule : IModule
{
    public const string BasePath = "posts-module";

    public string Name { get; } = "Posts";
    public string Path => BasePath;
    public void Register(IServiceCollection services)
    {
        services.AddCore();
        services.AddSignalR();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}