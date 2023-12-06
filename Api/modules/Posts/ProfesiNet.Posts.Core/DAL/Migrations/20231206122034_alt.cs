using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.Posts.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class alt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Org",
                table: "Creators",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Org",
                table: "Creators");
        }
    }
}
