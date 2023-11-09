using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Infrastructure.EntitiesConfiguration;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.StarDate).HasColumnType("date");
        builder.Property(x => x.EndDate).HasColumnType("date");
        builder.HasOne(x => x.Profile)
            .WithMany(x => x.Educations)
            .HasForeignKey(x => x.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}