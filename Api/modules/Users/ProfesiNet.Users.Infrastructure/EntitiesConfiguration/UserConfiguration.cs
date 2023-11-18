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
        builder.Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.EncodedPassword).HasMaxLength(500);
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Surname).HasMaxLength(50);
        builder.Property(x => x.Address).HasMaxLength(100);
        builder.Property(x => x.Bio).HasMaxLength(500);
        
    }
}