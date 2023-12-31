﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfesiNet.Posts.Core.DAL.Entities;

namespace ProfesiNet.Posts.Core.DAL.EntitiesConfiguration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreatorId).IsRequired();
        builder.Property(x => x.PostId).IsRequired();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.PublishedAt).IsRequired();
        
    }
}