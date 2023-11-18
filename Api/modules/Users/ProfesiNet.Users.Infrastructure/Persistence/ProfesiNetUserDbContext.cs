﻿using Microsoft.EntityFrameworkCore;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Infrastructure.Persistence;

public class ProfesiNetUserDbContext : DbContext
{
    public ProfesiNetUserDbContext(DbContextOptions<ProfesiNetUserDbContext> options) : base(options)
    {
    }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<ConnectionRequest> ConnectionRequests { get; set; }
    public DbSet<Following> Followings { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
  
}