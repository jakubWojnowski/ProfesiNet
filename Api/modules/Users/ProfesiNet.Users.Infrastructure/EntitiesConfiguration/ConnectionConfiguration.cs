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
            .WithMany(x => x.ProfileConnections)
            .HasForeignKey(x => x.ProfileId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Friend)
            .WithMany(x => x.FriendConnections)
            .HasForeignKey(x => x.FriendId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}