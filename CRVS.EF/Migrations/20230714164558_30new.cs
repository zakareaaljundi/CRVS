using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRVS.EF.Migrations
{
    /// <inheritdoc />
    public partial class _30new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBirthInHome",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "KabilaName",
                table: "BirthCertificates");

            migrationBuilder.AddColumn<bool>(
                name: "IsArabian",
                table: "Governorates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArabian",
                table: "Governorates");

            migrationBuilder.AddColumn<int>(
                name: "IsBirthInHome",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "KabilaName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
