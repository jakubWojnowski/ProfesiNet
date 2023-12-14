using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.Posts.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Org",
                table: "Creators",
                newName: "ProfilePicture");

            migrationBuilder.AddColumn<string>(
                name: "Followings",
                table: "Creators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Followings",
                table: "Creators");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "Creators",
                newName: "Org");
        }
    }
}
