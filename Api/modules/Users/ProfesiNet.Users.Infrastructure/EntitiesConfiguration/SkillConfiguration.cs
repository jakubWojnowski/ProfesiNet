using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Infrastructure.EntitiesConfiguration;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.HasOne(x => x.User).WithMany(x => x.Skills).HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}