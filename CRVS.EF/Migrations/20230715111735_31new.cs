using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRVS.EF.Migrations
{
    /// <inheritdoc />
    public partial class _31new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstStage",
                table: "BirthCertificates");

            migrationBuilder.RenameColumn(
                name: "SecondStage",
                table: "BirthCertificates",
                newName: "BiostatisticsStage");

            migrationBuilder.AlterColumn<string>(
                name: "ChildName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BiostatisticsStage",
                table: "BirthCertificates",
                newName: "SecondStage");

            migrationBuilder.AlterColumn<string>(
                name: "ChildName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FirstStage",
                table: "BirthCertificates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
