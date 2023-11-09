using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Infrastructure.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Surname).HasMaxLength(100);
        builder.HasOne(x => x.Profile)
            .WithOne(x => x.User)
            .HasForeignKey<Profile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}