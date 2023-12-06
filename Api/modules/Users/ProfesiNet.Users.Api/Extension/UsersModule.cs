using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Shared.Modules;
using ProfesiNet.Users.Application.Configurations.Extensions;
using ProfesiNet.Users.Infrastructure.Extension;

namespace ProfesiNet.Users.Api.Extension;

public class UsersModule : IModule
{
    public const string BasePath = "users-module";
    public string Name { get; } = "Users";
    public string Path => BasePath; 
    public void Register(IServiceCollection services)
    {
        services.AddInfrastructure(services.BuildServiceProvider().GetService<IConfiguration>() ?? throw new InvalidOperationException());
        services.AddApplication();
    }

    public void Use(IApplicationBuilder app)
    {
        throw new NotImplementedException();
    }
}