
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.LiveChats.Core.DAL.Persistence;
using ProfesiNet.LiveChats.Core.DAL.Repositories;
using ProfesiNet.Shared.MsSql;

namespace ProfesiNet.LiveChats.Core.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMsSql<ProfesiNetLiveChatsDbContext>();
        services.AddScoped<IChatMemberRepository, ChatMemberRepository>();
        return services;
    }
}