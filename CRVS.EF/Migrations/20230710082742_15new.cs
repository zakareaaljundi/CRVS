using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRVS.EF.Migrations
{
    /// <inheritdoc />
    public partial class _15new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Judiciary",
                table: "Users",
                newName: "Nahia");

            migrationBuilder.RenameColumn(
                name: "Directorate",
                table: "Users",
                newName: "Doh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nahia",
                table: "Users",
                newName: "Judiciary");

            migrationBuilder.RenameColumn(
                name: "Doh",
                table: "Users",
                newName: "Directorate");
        }
    }
}
