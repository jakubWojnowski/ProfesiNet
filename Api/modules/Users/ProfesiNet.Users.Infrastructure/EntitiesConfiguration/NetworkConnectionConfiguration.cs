using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Infrastructure.EntitiesConfiguration;

internal class NetworkConnectionConfiguration : IEntityTypeConfiguration<NetworkConnection>
{
    public void Configure(EntityTypeBuilder<NetworkConnection> builder)
    {
    
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Sender)
            .WithMany(x => x.NetworkConnectionsSend)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Target)
            .WithMany(x => x.NetworkConnectionsReceived)
            .HasForeignKey(x => x.TargetId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}