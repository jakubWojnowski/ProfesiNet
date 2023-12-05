using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.LiveChats.Core.DAL.Entities;

namespace ProfesiNet.LiveChats.Core.DAL.EntitiesConfiguration;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(100);
        
        
    }
}