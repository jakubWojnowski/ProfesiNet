using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Enums;

namespace ProfesiNet.Users.Infrastructure.EntitiesConfiguration;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        var converter = new EnumToStringConverter<PictureType>();
        builder.ToTable("Photos");

        builder.HasKey(x => x.Id);
        builder.Property(e =>e.PictureType).HasConversion(converter);


        builder.HasOne(x => x.User)
            .WithMany(x => x.Photos)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}