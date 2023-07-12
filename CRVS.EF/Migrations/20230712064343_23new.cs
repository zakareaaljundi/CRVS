using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRVS.EF.Migrations
{
    /// <inheritdoc />
    public partial class _23new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisableName",
                table: "Disables",
                newName: "QName");

            migrationBuilder.RenameColumn(
                name: "DisableId",
                table: "Disables",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "QID",
                table: "Disables",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QID",
                table: "Disables");

            migrationBuilder.RenameColumn(
                name: "QName",
                table: "Disables",
                newName: "DisableName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Disables",
                newName: "DisableId");
        }
    }
}
