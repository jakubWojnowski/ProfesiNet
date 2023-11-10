using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.Posts.Domain.Entities;

namespace ProfesiNet.Posts.Infrastructure.EntitiesConfiguration;

public class ShareConfiguration : IEntityTypeConfiguration<Share>
{
    public void Configure(EntityTypeBuilder<Share> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ProfileId).IsRequired();
        builder.Property(x => x.PostId).IsRequired();
        builder.Property(x => x.SharedAt).IsRequired();
    }
}