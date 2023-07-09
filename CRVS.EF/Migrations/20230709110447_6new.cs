using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRVS.EF.Migrations
{
    /// <inheritdoc />
    public partial class _6new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "GovernorateId", "GovernorateName" },
                values: new object[,]
                {
                    { 19, "محافظة1" },
                    { 20, "محافظة2" }
                });
        }
    }
}
