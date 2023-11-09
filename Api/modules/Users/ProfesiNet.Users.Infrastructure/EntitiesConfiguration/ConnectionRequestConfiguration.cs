using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Infrastructure.EntitiesConfiguration;

public class ConnectionRequestConfiguration : IEntityTypeConfiguration<ConnectionRequest>
{
    public void Configure(EntityTypeBuilder<ConnectionRequest> builder)
    {
        builder.HasKey(x => new {x.ProfileId, x.SenderId});
        builder.HasOne(x => x.Profile)
            .WithMany(x => x.ProfileConnectionRequests)
            .HasForeignKey(x => x.ProfileId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Sender)
            .WithMany(x => x.SenderConnectionRequests)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}