using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfesiNet.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterColumnInEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StarDate",
                table: "Educations",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Educations",
                newName: "Degree");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Educations",
                newName: "StarDate");

            migrationBuilder.RenameColumn(
                name: "Degree",
                table: "Educations",
                newName: "Grade");
        }
    }
}
