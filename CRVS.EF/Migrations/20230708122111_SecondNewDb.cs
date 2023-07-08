using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRVS.EF.Migrations
{
    /// <inheritdoc />
    public partial class SecondNewDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "BirthCertificates",
                newName: "SecondStage");

            migrationBuilder.AddColumn<bool>(
                name: "FirstStage",
                table: "BirthCertificates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstStage",
                table: "BirthCertificates");

            migrationBuilder.RenameColumn(
                name: "SecondStage",
                table: "BirthCertificates",
                newName: "IsCompleted");
        }
    }
}
