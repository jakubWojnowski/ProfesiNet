using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.Posts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Likes1",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes1",
                table: "Comments");
        }
    }
}
