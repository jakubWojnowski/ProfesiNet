using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.Users.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class networRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_User_FriendId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_User_ProfileId",
                table: "Connections");

            migrationBuilder.DropTable(
                name: "ConnectionRequests");

            migrationBuilder.DropTable(
                name: "Followings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_FriendId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Connections");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "Connections",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Connections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "Connections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "Connections",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TargetId",
                table: "Connections",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Connections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_SenderId",
                table: "Connections",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_TargetId",
                table: "Connections",
                column: "TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_User_SenderId",
                table: "Connections",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_User_TargetId",
                table: "Connections",
                column: "TargetId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_User_SenderId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_User_TargetId",
                table: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_SenderId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_TargetId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Connections");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Connections",
                newName: "FriendId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Connections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                columns: new[] { "ProfileId", "FriendId" });

            migrationBuilder.CreateTable(
                name: "ConnectionRequests",
                columns: table => new
                {
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionRequests", x => new { x.ReceiverId, x.SenderId });
                    table.ForeignKey(
                        name: "FK_ConnectionRequests_User_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConnectionRequests_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Followings",
                columns: table => new
                {
                    ObserverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followings", x => new { x.ObserverId, x.TargetId });
                    table.ForeignKey(
                        name: "FK_Followings_User_ObserverId",
                        column: x => x.ObserverId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Followings_User_TargetId",
                        column: x => x.TargetId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_FriendId",
                table: "Connections",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequests_SenderId",
                table: "ConnectionRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Followings_TargetId",
                table: "Followings",
                column: "TargetId");

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
        }
    }
}
