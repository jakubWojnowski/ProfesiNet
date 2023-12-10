using Microsoft.EntityFrameworkCore;
using ProfesiNet.LiveChats.Core.DAL.Entities;

namespace ProfesiNet.LiveChats.Core.DAL.Persistence;

public class ProfesiNetLiveChatsDbContext : DbContext
{
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatMember> ChatMembers { get; set; }
    public DbSet<Message> ChatMessages { get; set; }

    public ProfesiNetLiveChatsDbContext(DbContextOptions<ProfesiNetLiveChatsDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}