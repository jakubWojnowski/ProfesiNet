using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserDatabaseRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_Profiles_ProfileId",
                table: "ConnectionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_Profiles_SenderId",
                table: "ConnectionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Profiles_FriendId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Profiles_ProfileId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Profiles_ProfileId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Profiles_ProfileId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Profiles_ObserverId",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Profiles_TargetId",
                table: "Followings");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Experiences",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_ProfileId",
                table: "Experiences",
                newName: "IX_Experiences_UserId");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Educations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_ProfileId",
                table: "Educations",
                newName: "IX_Educations_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "User",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_User_ProfileId",
                table: "ConnectionRequests",
                column: "ProfileId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_User_SenderId",
                table: "ConnectionRequests",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_User_FriendId",
                table: "Connections",
                column: "FriendId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_User_ProfileId",
                table: "Connections",
                column: "ProfileId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_User_UserId",
                table: "Educations",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_User_UserId",
                table: "Experiences",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_User_ObserverId",
                table: "Followings",
                column: "ObserverId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_User_TargetId",
                table: "Followings",
                column: "TargetId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_User_ProfileId",
                table: "ConnectionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_User_SenderId",
                table: "ConnectionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_User_FriendId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_User_ProfileId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_User_UserId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_User_UserId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_User_ObserverId",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_User_TargetId",
                table: "Followings");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Experiences",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                newName: "IX_Experiences_ProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Educations",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_UserId",
                table: "Educations",
                newName: "IX_Educations_ProfileId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_Profiles_ProfileId",
                table: "ConnectionRequests",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_Profiles_SenderId",
                table: "ConnectionRequests",
                column: "SenderId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Profiles_FriendId",
                table: "Connections",
                column: "FriendId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Profiles_ProfileId",
                table: "Connections",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Profiles_ProfileId",
                table: "Educations",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Profiles_ProfileId",
                table: "Experiences",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_Profiles_ObserverId",
                table: "Followings",
                column: "ObserverId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_Profiles_TargetId",
                table: "Followings",
                column: "TargetId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
