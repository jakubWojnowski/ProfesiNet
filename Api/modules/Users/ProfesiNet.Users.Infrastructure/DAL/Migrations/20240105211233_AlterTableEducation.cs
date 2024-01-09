using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.Users.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Educations",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Educations",
                newName: "Description");
        }
    }
}
