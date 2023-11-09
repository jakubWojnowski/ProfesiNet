using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Infrastructure.EntitiesConfiguration;

public class FollowingConfiguration : IEntityTypeConfiguration<Following>
{
    public void Configure(EntityTypeBuilder<Following> builder)
    {
        builder.HasKey(x => new {x.ObserverId, x.TargetId});
        
        builder.HasOne(x => x.Observer)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.ObserverId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Target)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.TargetId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
    }
}