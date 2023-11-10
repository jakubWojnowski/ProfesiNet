using Microsoft.EntityFrameworkCore;
using ProfesiNet.Posts.Domain.Entities;

namespace ProfesiNet.Posts.Infrastructure.Persistence;

public class ProfesiNetPostDbContext : DbContext
{
    public ProfesiNetPostDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Share> Shares { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}