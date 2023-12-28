using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.Posts.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AlterTablePostsAddColumnsNameImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Media",
                table: "Posts",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Posts",
                newName: "Media");
        }
    }
}
