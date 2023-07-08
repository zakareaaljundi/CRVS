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
    [Migration("20230708082611_NewDB")]
    partial class NewDB
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

                    b.Property<bool>("Approval")
                        .HasColumnType("bit");

                    b.Property<string>("ChildName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Creator")
                        .HasColumnType("int");

                    b.Property<string>("Directorate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacilityType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherReligion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Governorate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HealthInstitution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Judiciary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherReligion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Venu")
                        .HasColumnType("int");

                    b.Property<string>("Village")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BirthCertificateId");

                    b.ToTable("BirthCertificates");
                });

            modelBuilder.Entity("CRVS.Core.Models.Directorate", b =>
                {
                    b.Property<int>("DirectorateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DirectorateId"));

                    b.Property<string>("DirectorateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GovernorateId")
                        .HasColumnType("int");

                    b.HasKey("DirectorateId");

                    b.ToTable("Directorates");

                    b.HasData(
                        new
                        {
                            DirectorateId = 1,
                            DirectorateName = "دائرة1",
                            GovernorateId = 1
                        },
                        new
                        {
                            DirectorateId = 2,
                            DirectorateName = "دائرة1",
                            GovernorateId = 1
                        },
                        new
                        {
                            DirectorateId = 3,
                            DirectorateName = "دائرة2",
                            GovernorateId = 2
                        },
                        new
                        {
                            DirectorateId = 4,
                            DirectorateName = "دائرة2",
                            GovernorateId = 2
                        });
                });

            modelBuilder.Entity("CRVS.Core.Models.District", b =>
                {
                    b.Property<int>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DistrictId"));

                    b.Property<string>("DistrictName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JudiciaryId")
                        .HasColumnType("int");

                    b.HasKey("DistrictId");

                    b.ToTable("Districts");

                    b.HasData(
                        new
                        {
                            DistrictId = 1,
                            DistrictName = "الناحية1",
                            JudiciaryId = 1
                        },
                        new
                        {
                            DistrictId = 2,
                            DistrictName = "2الناحية",
                            JudiciaryId = 2
                        },
                        new
                        {
                            DistrictId = 3,
                            DistrictName = "الناحية2",
                            JudiciaryId = 2
                        },
                        new
                        {
                            DistrictId = 4,
                            DistrictName = "الناحية3",
                            JudiciaryId = 3
                        },
                        new
                        {
                            DistrictId = 5,
                            DistrictName = "الناحية4",
                            JudiciaryId = 4
                        },
                        new
                        {
                            DistrictId = 6,
                            DistrictName = "الناحية4",
                            JudiciaryId = 4
                        },
                        new
                        {
                            DistrictId = 7,
                            DistrictName = "الناحية5",
                            JudiciaryId = 5
                        });
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

                    b.HasData(
                        new
                        {
                            FacilityTypeId = 1,
                            FacilityTypeName = "مستشفى"
                        },
                        new
                        {
                            FacilityTypeId = 2,
                            FacilityTypeName = "مركز"
                        },
                        new
                        {
                            FacilityTypeId = 3,
                            FacilityTypeName = "مكتب"
                        });
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

                    b.HasData(
                        new
                        {
                            GovernorateId = 1,
                            GovernorateName = "محافظة1"
                        },
                        new
                        {
                            GovernorateId = 2,
                            GovernorateName = "محافظة2"
                        });
                });

            modelBuilder.Entity("CRVS.Core.Models.HealthInstitution", b =>
                {
                    b.Property<int>("HealthInstitutionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HealthInstitutionId"));

                    b.Property<int>("FacilityTypeId")
                        .HasColumnType("int");

                    b.Property<string>("HealthInstitutionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HealthInstitutionId");

                    b.ToTable("HealthInstitutions");

                    b.HasData(
                        new
                        {
                            HealthInstitutionId = 1,
                            FacilityTypeId = 1,
                            HealthInstitutionName = "مستشفى1"
                        },
                        new
                        {
                            HealthInstitutionId = 2,
                            FacilityTypeId = 1,
                            HealthInstitutionName = "مستشفى2"
                        },
                        new
                        {
                            HealthInstitutionId = 3,
                            FacilityTypeId = 2,
                            HealthInstitutionName = "مركز1"
                        },
                        new
                        {
                            HealthInstitutionId = 4,
                            FacilityTypeId = 2,
                            HealthInstitutionName = "مركز2"
                        },
                        new
                        {
                            HealthInstitutionId = 5,
                            FacilityTypeId = 3,
                            HealthInstitutionName = "مكتب1"
                        },
                        new
                        {
                            HealthInstitutionId = 6,
                            FacilityTypeId = 3,
                            HealthInstitutionName = "مكتب2"
                        });
                });

            modelBuilder.Entity("CRVS.Core.Models.Judiciary", b =>
                {
                    b.Property<int>("JudiciaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JudiciaryId"));

                    b.Property<int>("DirectorateId")
                        .HasColumnType("int");

                    b.Property<string>("JudiciaryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JudiciaryId");

                    b.ToTable("Judiciaries");

                    b.HasData(
                        new
                        {
                            JudiciaryId = 1,
                            DirectorateId = 1,
                            JudiciaryName = "قضاء1"
                        },
                        new
                        {
                            JudiciaryId = 2,
                            DirectorateId = 2,
                            JudiciaryName = "قضاء2"
                        },
                        new
                        {
                            JudiciaryId = 3,
                            DirectorateId = 2,
                            JudiciaryName = "قضاء2"
                        },
                        new
                        {
                            JudiciaryId = 4,
                            DirectorateId = 3,
                            JudiciaryName = "قضاء3"
                        },
                        new
                        {
                            JudiciaryId = 5,
                            DirectorateId = 4,
                            JudiciaryName = "قضاء4"
                        });
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

                    b.Property<string>("Directorate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
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

                    b.Property<string>("Judiciary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RegisterDate")
                        .HasColumnType("datetime2");

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
