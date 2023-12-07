﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfesiNet.Users.Infrastructure.Persistence;

#nullable disable

namespace ProfesiNet.Users.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(ProfesiNetUserDbContext))]
    [Migration("20231207125452_EntitiesRefactor")]
    partial class EntitiesRefactor
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Certificate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Connection", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FriendId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProfileId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.ConnectionRequest", b =>
                {
                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ReceiverId", "SenderId");

                    b.HasIndex("SenderId");

                    b.ToTable("ConnectionRequests");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Education", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("FieldOfStudy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Experience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Company")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Position")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Following", b =>
                {
                    b.Property<Guid>("ObserverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ObserverId", "TargetId");

                    b.HasIndex("TargetId");

                    b.ToTable("Followings");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Bio")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EncodedPassword")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Certificate", b =>
                {
                    b.HasOne("ProfesiNet.Users.Domain.Entities.User", "User")
                        .WithMany("Certificates")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Connection", b =>
                {
                    b.HasOne("ProfesiNet.Users.Domain.Entities.User", "Friend")
                        .WithMany("FriendConnections")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProfesiNet.Users.Domain.Entities.User", "Profile")
                        .WithMany("ProfileConnections")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Friend");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.ConnectionRequest", b =>
                {
                    b.HasOne("ProfesiNet.Users.Domain.Entities.User", "Receiver")
                        .WithMany("ProfileConnectionRequests")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProfesiNet.Users.Domain.Entities.User", "Sender")
                        .WithMany("SenderConnectionRequests")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Education", b =>
                {
                    b.HasOne("ProfesiNet.Users.Domain.Entities.User", "User")
                        .WithMany("Educations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Experience", b =>
                {
                    b.HasOne("ProfesiNet.Users.Domain.Entities.User", "User")
                        .WithMany("Experiences")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Following", b =>
                {
                    b.HasOne("ProfesiNet.Users.Domain.Entities.User", "Observer")
                        .WithMany("ObserverFollowings")
                        .HasForeignKey("ObserverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProfesiNet.Users.Domain.Entities.User", "Target")
                        .WithMany("TargetFollowings")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Observer");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.Skill", b =>
                {
                    b.HasOne("ProfesiNet.Users.Domain.Entities.User", "User")
                        .WithMany("Skills")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProfesiNet.Users.Domain.Entities.User", b =>
                {
                    b.Navigation("Certificates");

                    b.Navigation("Educations");

                    b.Navigation("Experiences");

                    b.Navigation("FriendConnections");

                    b.Navigation("ObserverFollowings");

                    b.Navigation("ProfileConnectionRequests");

                    b.Navigation("ProfileConnections");

                    b.Navigation("SenderConnectionRequests");

                    b.Navigation("Skills");

                    b.Navigation("TargetFollowings");
                });
#pragma warning restore 612, 618
        }
    }
}
