using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.Posts.Core.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharedAt",
                table: "Shares");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PostLikes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CommentLikes");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Shares",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Posts",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Posts",
                newName: "PublishedAt");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "PostLikes",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Comments",
                newName: "PublishedAt");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Comments",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "CommentLikes",
                newName: "CreatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Shares",
                newName: "ProfileId");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "Posts",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Posts",
                newName: "ProfileId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "PostLikes",
                newName: "ProfileId");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "Comments",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Comments",
                newName: "ProfileId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "CommentLikes",
                newName: "ProfileId");

            migrationBuilder.AddColumn<DateTime>(
                name: "SharedAt",
                table: "Shares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Posts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PostLikes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CommentLikes",
                type: "datetime2",
                nullable: true);
        }
    }
}
