using Microsoft.EntityFrameworkCore;
using ProfesiNet.LiveChats.Core.Entities;

namespace ProfesiNet.LiveChats.Core.Persistence;

public class ProfesiNetLiveChatsDbContext : DbContext
{
    public ProfesiNetLiveChatsDbContext(DbContextOptions<ProfesiNetLiveChatsDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatMember> ChatMembers { get; set; }
    public DbSet<Message> ChatMessages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
    
}