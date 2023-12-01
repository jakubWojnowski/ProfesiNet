using Microsoft.EntityFrameworkCore;
using ProfesiNet.Posts.Core.Entities;

namespace ProfesiNet.Posts.Core.Persistence;

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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}