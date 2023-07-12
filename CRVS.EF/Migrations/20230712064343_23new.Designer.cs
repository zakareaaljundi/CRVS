﻿// <auto-generated />
using System;
using CRVS.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRVS.EF.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230712064343_23new")]
    partial class _23new
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CRVS.Core.Models.BirthCertificate", b =>
                {
                    b.Property<int>("BirthCertificateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BirthCertificateId"));

                    b.Property<int>("Alive")
                        .HasColumnType("int");

                    b.Property<bool>("Approval")
                        .HasColumnType("bit");

                    b.Property<decimal?>("BabyWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("BirthHour")
                        .HasColumnType("datetime2");

                    b.Property<int>("BirthOccurredBy")
                        .HasColumnType("int");

                    b.Property<string>("BirthPerformerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BirthPerformerWorkingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BirthType")
                        .HasColumnType("int");

                    b.Property<int>("BornAliveThenDied")
                        .HasColumnType("int");

                    b.Property<int>("BornDisable")
                        .HasColumnType("int");

                    b.Property<string>("ChildName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CivilStatusDirectorate")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Creator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("DOBText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisabledType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentType")
                        .HasColumnType("int");

                    b.Property<string>("Doh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationOfPregnancy")
                        .HasColumnType("int");

                    b.Property<string>("FacilityType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyDOH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyDistrict")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyGovernorate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyHomeNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyMahala")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyNahia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyPHC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilySector")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyZigag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherAge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FatherDOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FatherFName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherLName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherMName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherMobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherNationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherReligion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FirstStage")
                        .HasColumnType("bit");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Governorate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GovernorateCivilStatusDirectorate")
                        .HasColumnType("int");

                    b.Property<string>("HealthId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HealthInstitution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HospitalManagerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HospitalManagerSig")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgBirthCertificate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgFatherUnifiedNationalIdBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgFatherUnifiedNationalIdFront")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgMarriageCertificate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgMotherUnifiedNationalIdBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgMotherUnifiedNationalIdFront")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgResidencyCardBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgResidencyCardFront")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InformerJobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InformerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("KabilaName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KinshipOfTheNewborn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicenseNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LicenseYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("MotherAge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MotherDOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("MotherFName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherLName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherMName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherMobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherNationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherReligion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nahia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NationalId")
                        .HasColumnType("int");

                    b.Property<int>("NationalIdFor")
                        .HasColumnType("int");

                    b.Property<int>("NoAbortion")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfBirth")
                        .HasColumnType("int");

                    b.Property<int>("PageNumber")
                        .HasColumnType("int");

                    b.Property<string>("PassportNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlaceOfBirth")
                        .HasColumnType("int");

                    b.Property<int>("RationCard")
                        .HasColumnType("int");

                    b.Property<int>("RecordNumber")
                        .HasColumnType("int");

                    b.Property<bool>("Relative")
                        .HasColumnType("bit");

                    b.Property<bool>("SecondStage")
                        .HasColumnType("bit");

                    b.Property<int>("StillBirth")
                        .HasColumnType("int");

                    b.Property<string>("Village")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BirthCertificateId");

                    b.ToTable("BirthCertificates");
                });

            modelBuilder.Entity("CRVS.Core.Models.Disable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("QID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Disables");
                });

            modelBuilder.Entity("CRVS.Core.Models.District", b =>
                {
                    b.Property<int>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DistrictId"));

                    b.Property<string>("DistrictName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DohId")
                        .HasColumnType("int");

                    b.Property<int>("GovernorateId")
                        .HasColumnType("int");

                    b.HasKey("DistrictId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("CRVS.Core.Models.Doh", b =>
                {
                    b.Property<int>("DohId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DohId"));

                    b.Property<string>("DohName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GovernorateId")
                        .HasColumnType("int");

                    b.HasKey("DohId");

                    b.ToTable("Dohs");
                });

            modelBuilder.Entity("CRVS.Core.Models.FacilityType", b =>
                {
                    b.Property<int>("FacilityTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacilityTypeId"));

                    b.Property<string>("FacilityTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FacilityTypeId");

                    b.ToTable("FacilityTypes");
                });

            modelBuilder.Entity("CRVS.Core.Models.Governorate", b =>
                {
                    b.Property<int>("GovernorateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GovernorateId"));

                    b.Property<string>("GovernorateName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GovernorateId");

                    b.ToTable("Governorates");
                });

            modelBuilder.Entity("CRVS.Core.Models.HealthInstitution", b =>
                {
                    b.Property<int>("HealthInstitutionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HealthInstitutionId"));

                    b.Property<int>("DohId")
                        .HasColumnType("int");

                    b.Property<int>("FacilityTypeId")
                        .HasColumnType("int");

                    b.Property<int>("GovernorateId")
                        .HasColumnType("int");

                    b.Property<string>("HealthInstitutionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HealthInstitutionId");

                    b.ToTable("HealthInstitutions");
                });

            modelBuilder.Entity("CRVS.Core.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<string>("JobName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("CRVS.Core.Models.Nahia", b =>
                {
                    b.Property<int>("NahiaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NahiaId"));

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<int>("DohId")
                        .HasColumnType("int");

                    b.Property<int>("GovernorateId")
                        .HasColumnType("int");

                    b.Property<string>("NahiaName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NahiaId");

                    b.ToTable("Nahias");
                });

            modelBuilder.Entity("CRVS.Core.Models.Nationality", b =>
                {
                    b.Property<int>("NationalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NationalityId"));

                    b.Property<string>("NationalityName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NationalityId");

                    b.ToTable("Nationalities");
                });

            modelBuilder.Entity("CRVS.Core.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<int>("CertificateId")
                        .HasColumnType("int");

                    b.Property<string>("CurrentUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DAT")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadLine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsGoodFeedBack")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSettingMessage")
                        .HasColumnType("bit");

                    b.HasKey("NotificationId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("CRVS.Core.Models.Religion", b =>
                {
                    b.Property<int>("ReligionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReligionId"));

                    b.Property<string>("ReligionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReligionId");

                    b.ToTable("Religions");
                });

            modelBuilder.Entity("CRVS.Core.Models.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AddPermission")
                        .HasColumnType("bit");

                    b.Property<bool>("ApprovalPermission")
                        .HasColumnType("bit");

                    b.Property<bool>("DeletePermission")
                        .HasColumnType("bit");

                    b.Property<bool>("EditPermission")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadPermission")
                        .HasColumnType("bit");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("CRVS.Core.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Doh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacilityType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Governorate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HealthInstitution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nahia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Village")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
