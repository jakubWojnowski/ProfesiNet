using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using ProfesiNet.Posts.Core.DAL.Persistence;
using ProfesiNet.Posts.Core.DAL.Repositories;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Policies;
using ProfesiNet.Posts.Core.Services;
using ProfesiNet.Shared.MsSql;

[assembly: InternalsVisibleTo("ProfesiNet.Posts.Api")]
namespace ProfesiNet.Posts.Core.Extension;

internal static class ServiceCollectionExtension 
{
    public static IServiceCollection AddCore (this IServiceCollection services)
    {
   
        services.AddMsSql<ProfesiNetPostDbContext>();
        //Posts
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IPostCannotBeEmptyPolicy, PostCannotBeEmptyPolicy>();
        
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
        
        //creators
        services.AddScoped<ICreatorRepository, CreatorRepository>();
        
        return services;
        
       
    }
}