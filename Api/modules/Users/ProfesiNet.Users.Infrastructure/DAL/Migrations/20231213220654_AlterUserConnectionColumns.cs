using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.Users.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AlterUserConnectionColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NetworkConnectionInvitations",
                table: "Users",
                newName: "NetworkConnectionInvitationsSent");

            migrationBuilder.AddColumn<string>(
                name: "NetworkConnectionInvitationsReceived",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetworkConnectionInvitationsReceived",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "NetworkConnectionInvitationsSent",
                table: "Users",
                newName: "NetworkConnectionInvitations");
        }
    }
}
