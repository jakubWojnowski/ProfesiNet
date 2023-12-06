using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.LiveChats.Core.Extension;
using ProfesiNet.Shared.Modules;

namespace ProfesiNet.LiveChats.Api.Extension;

public class LiveChatsModule : IModule
{
    public const string BasePath = "live-chats-module";
    public string Name { get; } = "LiveChats";
    public string Path => BasePath;
    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    
    }
}