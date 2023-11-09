using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Infrastructure.EntitiesConfiguration;

public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder.HasKey(x => new {x.ProfileId, x.FriendId});
        
        builder.HasOne(x => x.Profile)
            .WithMany(x => x.Connections)
            .HasForeignKey(x => x.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Friend)
            .WithMany(x => x.Connections)
            .HasForeignKey(x => x.FriendId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}