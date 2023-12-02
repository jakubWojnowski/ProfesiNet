using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Persistence;
using ProfesiNet.Posts.Core.Policies;
using ProfesiNet.Posts.Core.Repositories;
using ProfesiNet.Posts.Core.Services;

[assembly: InternalsVisibleTo("ProfesiNet.Posts.Api")]
namespace ProfesiNet.Posts.Core.Extension;

internal static class ServiceCollectionExtension 
{
    public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProfesiNetPostDbContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("ProfesiNet"));
        });
        
        //Posts
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostService, PostService>();
        
        //comments
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICommentService, CommentService>();
        
        //comment likes
        services.AddScoped<ICommentLikeRepository, CommentLikeRepository>();
        services.AddScoped<ICommentLikeService, CommentLikeService>();
        services.AddScoped<IUserCantAddLikeToCommentPolicy, UserCantAddLikeToCommentPolicyPolicy>();
        
        //post likes
        services.AddScoped<IPostLikeRepository, PostLikeRepository>();
        services.AddScoped<IPostLikeService, PostLikeService>();
        services.AddScoped<IUserCantAddLikeToPostPolicy, UserCantAddLikeToPostPolicy>();
        
        //shares
        services.AddScoped<IShareRepository, ShareRepository>();
        services.AddScoped<IShareService, ShareService>();
        services.AddScoped<IUserCantSharePolicy, UserCantSharePolicy>();
        
        return services;
        
       
    }
}