using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Infrastructure.EntitiesConfiguration;

internal class FollowingConfiguration : IEntityTypeConfiguration<Following>
{
    public void Configure(EntityTypeBuilder<Following> builder)
    {
        builder.HasKey(x => new {x.ObserverId, x.TargetId});
        
        builder.HasOne(x => x.Observer)
            .WithMany(x => x.ObserverFollowings)
            .HasForeignKey(x => x.ObserverId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Target)
            .WithMany(x => x.TargetFollowings)
            .HasForeignKey(x => x.TargetId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
    }
}