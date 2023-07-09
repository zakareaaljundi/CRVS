using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRVS.EF.Migrations
{
    /// <inheritdoc />
    public partial class _2newMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Directorates",
                keyColumn: "DirectorateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Directorates",
                keyColumn: "DirectorateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Directorates",
                keyColumn: "DirectorateId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Directorates",
                keyColumn: "DirectorateId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "DistrictId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "DistrictId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "DistrictId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "DistrictId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "DistrictId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "DistrictId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "DistrictId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FacilityTypes",
                keyColumn: "FacilityTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FacilityTypes",
                keyColumn: "FacilityTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FacilityTypes",
                keyColumn: "FacilityTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HealthInstitutions",
                keyColumn: "HealthInstitutionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HealthInstitutions",
                keyColumn: "HealthInstitutionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HealthInstitutions",
                keyColumn: "HealthInstitutionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HealthInstitutions",
                keyColumn: "HealthInstitutionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HealthInstitutions",
                keyColumn: "HealthInstitutionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HealthInstitutions",
                keyColumn: "HealthInstitutionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Judiciaries",
                keyColumn: "JudiciaryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Judiciaries",
                keyColumn: "JudiciaryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Judiciaries",
                keyColumn: "JudiciaryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Judiciaries",
                keyColumn: "JudiciaryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Judiciaries",
                keyColumn: "JudiciaryId",
                keyValue: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Directorates",
                columns: new[] { "DirectorateId", "DirectorateName", "GovernorateId" },
                values: new object[,]
                {
                    { 1, "دائرة1", 1 },
                    { 2, "دائرة1", 1 },
                    { 3, "دائرة2", 2 },
                    { 4, "دائرة2", 2 }
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "DistrictId", "DistrictName", "JudiciaryId" },
                values: new object[,]
                {
                    { 1, "الناحية1", 1 },
                    { 2, "2الناحية", 2 },
                    { 3, "الناحية2", 2 },
                    { 4, "الناحية3", 3 },
                    { 5, "الناحية4", 4 },
                    { 6, "الناحية4", 4 },
                    { 7, "الناحية5", 5 }
                });

            migrationBuilder.InsertData(
                table: "FacilityTypes",
                columns: new[] { "FacilityTypeId", "FacilityTypeName" },
                values: new object[,]
                {
                    { 1, "مستشفى" },
                    { 2, "مركز" },
                    { 3, "مكتب" }
                });

            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "GovernorateId", "GovernorateName" },
                values: new object[,]
                {
                    { 1, "محافظة1" },
                    { 2, "محافظة2" }
                });

            migrationBuilder.InsertData(
                table: "HealthInstitutions",
                columns: new[] { "HealthInstitutionId", "FacilityTypeId", "HealthInstitutionName" },
                values: new object[,]
                {
                    { 1, 1, "مستشفى1" },
                    { 2, 1, "مستشفى2" },
                    { 3, 2, "مركز1" },
                    { 4, 2, "مركز2" },
                    { 5, 3, "مكتب1" },
                    { 6, 3, "مكتب2" }
                });

            migrationBuilder.InsertData(
                table: "Judiciaries",
                columns: new[] { "JudiciaryId", "DirectorateId", "JudiciaryName" },
                values: new object[,]
                {
                    { 1, 1, "قضاء1" },
                    { 2, 2, "قضاء2" },
                    { 3, 2, "قضاء2" },
                    { 4, 3, "قضاء3" },
                    { 5, 4, "قضاء4" }
                });
        }
    }
}
