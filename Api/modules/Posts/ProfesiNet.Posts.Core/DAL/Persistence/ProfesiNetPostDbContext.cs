using Microsoft.EntityFrameworkCore;
using ProfesiNet.Posts.Core.DAL.Entities;

namespace ProfesiNet.Posts.Core.DAL.Persistence;

public class ProfesiNetPostDbContext : DbContext
{
    public ProfesiNetPostDbContext(DbContextOptions<ProfesiNetPostDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Share> Shares { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
    public DbSet<CommentLike> CommentLikes { get; set; }
    public DbSet<Creator> Creators { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}