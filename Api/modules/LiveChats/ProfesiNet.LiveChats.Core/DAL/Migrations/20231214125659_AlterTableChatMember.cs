using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.LiveChats.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableChatMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstUserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "SecondUserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "ChatMessages");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatMemberId",
                table: "ChatMessages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NetworkConnections",
                table: "ChatMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "ChatMembers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatMemberId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "NetworkConnections",
                table: "ChatMembers");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "ChatMembers");

            migrationBuilder.AddColumn<Guid>(
                name: "FirstUserId",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SecondUserId",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiverId",
                table: "ChatMessages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "ChatMessages",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
