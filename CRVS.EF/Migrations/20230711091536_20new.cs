using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRVS.EF.Migrations
{
    /// <inheritdoc />
    public partial class _20new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Venu",
                table: "BirthCertificates",
                newName: "StillBirth");

            migrationBuilder.RenameColumn(
                name: "MotherName",
                table: "BirthCertificates",
                newName: "PassportNo");

            migrationBuilder.RenameColumn(
                name: "Judiciary",
                table: "BirthCertificates",
                newName: "Nahia");

            migrationBuilder.RenameColumn(
                name: "FatherName",
                table: "BirthCertificates",
                newName: "MotherNationality");

            migrationBuilder.RenameColumn(
                name: "Directorate",
                table: "BirthCertificates",
                newName: "MotherMobile");

            migrationBuilder.AddColumn<int>(
                name: "Alive",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BHour",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BabyWeight",
                table: "BirthCertificates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "BirthOccurredBy",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BirthPerformerName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthPerformerWorkingAddress",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BirthType",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BornAliveThenDied",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BornDisable",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CivilStatusDirectorate",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "BirthCertificates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "BirthCertificates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DOBText",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisabledType",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Doh",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationOfPregnancy",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FamilyDOH",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyDistrict",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyGovernorate",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyHomeNo",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyMahala",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyNahia",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyPHC",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilySector",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyZigag",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherAge",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FatherDOB",
                table: "BirthCertificates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FatherFName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherLName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherMName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherMobile",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherNationality",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GovernorateCivilStatusDirectorate",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HealthId",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalManagerName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalManagerSig",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgBirthCertificate",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgFatherUnifiedNationalIdBack",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgFatherUnifiedNationalIdFront",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgMarriageCertificate",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgMotherUnifiedNationalIdBack",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgMotherUnifiedNationalIdFront",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgResidencyCardBack",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgResidencyCardFront",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InformerJobTitle",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InformerName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BirthCertificates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "BirthCertificates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "KabilaName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KinshipOfTheNewborn",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseNo",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenseYear",
                table: "BirthCertificates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MotherAge",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MotherDOB",
                table: "BirthCertificates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MotherFName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherLName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherMName",
                table: "BirthCertificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalId",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NationalIdFor",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoAbortion",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBirth",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageNumber",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlaceOfBirth",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RationCard",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecordNumber",
                table: "BirthCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Relative",
                table: "BirthCertificates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alive",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "BHour",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "BabyWeight",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "BirthOccurredBy",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "BirthPerformerName",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "BirthPerformerWorkingAddress",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "BirthType",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "BornAliveThenDied",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "BornDisable",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "CivilStatusDirectorate",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "DOBText",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "DisabledType",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "Doh",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "DurationOfPregnancy",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FamilyDOH",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FamilyDistrict",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FamilyGovernorate",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FamilyHomeNo",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FamilyMahala",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FamilyNahia",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FamilyPHC",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FamilySector",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FamilyZigag",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FatherAge",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FatherDOB",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FatherFName",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FatherLName",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FatherMName",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FatherMobile",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "FatherNationality",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "GovernorateCivilStatusDirectorate",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "HealthId",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "HospitalManagerName",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "HospitalManagerSig",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "ImgBirthCertificate",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "ImgFatherUnifiedNationalIdBack",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "ImgFatherUnifiedNationalIdFront",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "ImgMarriageCertificate",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "ImgMotherUnifiedNationalIdBack",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "ImgMotherUnifiedNationalIdFront",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "ImgResidencyCardBack",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "ImgResidencyCardFront",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "InformerJobTitle",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "InformerName",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "KabilaName",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "KinshipOfTheNewborn",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "LicenseNo",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "LicenseYear",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "MotherAge",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "MotherDOB",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "MotherFName",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "MotherLName",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "MotherMName",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "NationalIdFor",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "NoAbortion",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "NumberOfBirth",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "PageNumber",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirth",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "RationCard",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "RecordNumber",
                table: "BirthCertificates");

            migrationBuilder.DropColumn(
                name: "Relative",
                table: "BirthCertificates");

            migrationBuilder.RenameColumn(
                name: "StillBirth",
                table: "BirthCertificates",
                newName: "Venu");

            migrationBuilder.RenameColumn(
                name: "PassportNo",
                table: "BirthCertificates",
                newName: "MotherName");

            migrationBuilder.RenameColumn(
                name: "Nahia",
                table: "BirthCertificates",
                newName: "Judiciary");

            migrationBuilder.RenameColumn(
                name: "MotherNationality",
                table: "BirthCertificates",
                newName: "FatherName");

            migrationBuilder.RenameColumn(
                name: "MotherMobile",
                table: "BirthCertificates",
                newName: "Directorate");
        }
    }
}
