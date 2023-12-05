using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.LiveChats.Core.DAL.Entities;

namespace ProfesiNet.LiveChats.Core.DAL.EntitiesConfiguration;

public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder.ToTable("ChatMembers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Surname).HasMaxLength(50);
        builder.HasMany(x => x.Chats).WithMany(x => x.ChatMembers);
    }
}