using Microsoft.EntityFrameworkCore;
using CRVS.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.EF
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>option) : base(option)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<BirthCertificate> BirthCertificates { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Directorate> Directorates { get; set; }
        public DbSet<Judiciary> Judiciaries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<FacilityType> FacilityTypes { get; set; }
        public DbSet<HealthInstitution> HealthInstitutions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Governorate>().HasData(
                new Governorate { GovernorateId = 1, GovernorateName = "محافظة1" },
                new Governorate { GovernorateId = 2, GovernorateName = "محافظة2" }
            );
            modelBuilder.Entity<Directorate>().HasData(
                new Directorate { DirectorateId = 1, GovernorateId = 1, DirectorateName = "دائرة1" },
                new Directorate { DirectorateId = 2, GovernorateId = 1, DirectorateName = "دائرة1" },
                new Directorate { DirectorateId = 3, GovernorateId = 2, DirectorateName = "دائرة2" },
                new Directorate { DirectorateId = 4, GovernorateId = 2, DirectorateName = "دائرة2" }
            );
            modelBuilder.Entity<Judiciary>().HasData(
                new Judiciary { JudiciaryId = 1, DirectorateId = 1, JudiciaryName = "قضاء1" },
                new Judiciary { JudiciaryId = 2, DirectorateId = 2, JudiciaryName = "قضاء2" },
                new Judiciary { JudiciaryId = 3, DirectorateId = 2, JudiciaryName = "قضاء2" },
                new Judiciary { JudiciaryId = 4, DirectorateId = 3, JudiciaryName = "قضاء3" },
                new Judiciary { JudiciaryId = 5, DirectorateId = 4, JudiciaryName = "قضاء4" }
            );
            modelBuilder.Entity<District>().HasData(
                new District { DistrictId = 1, JudiciaryId = 1, DistrictName = "الناحية1" },
                new District { DistrictId = 2, JudiciaryId = 2, DistrictName = "2الناحية" },
                new District { DistrictId = 3, JudiciaryId = 2, DistrictName = "الناحية2" },
                new District { DistrictId = 4, JudiciaryId = 3, DistrictName = "الناحية3" },
                new District { DistrictId = 5, JudiciaryId = 4, DistrictName = "الناحية4" },
                new District { DistrictId = 6, JudiciaryId = 4, DistrictName = "الناحية4" },
                new District { DistrictId = 7, JudiciaryId = 5, DistrictName = "الناحية5" }
            );
            modelBuilder.Entity<FacilityType>().HasData(
                new FacilityType { FacilityTypeId = 1, FacilityTypeName = "مستشفى" },
                new FacilityType { FacilityTypeId = 2, FacilityTypeName = "مركز" },
                new FacilityType { FacilityTypeId = 3, FacilityTypeName = "مكتب" }
            );
            modelBuilder.Entity<HealthInstitution>().HasData(
                new HealthInstitution { HealthInstitutionId = 1, FacilityTypeId = 1, HealthInstitutionName = "مستشفى1" },
                new HealthInstitution { HealthInstitutionId = 2, FacilityTypeId = 1, HealthInstitutionName = "مستشفى2" },
                new HealthInstitution { HealthInstitutionId = 3, FacilityTypeId = 2, HealthInstitutionName = "مركز1" },
                new HealthInstitution { HealthInstitutionId = 4, FacilityTypeId = 2, HealthInstitutionName = "مركز2" },
                new HealthInstitution { HealthInstitutionId = 5, FacilityTypeId = 3, HealthInstitutionName = "مكتب1" },
                new HealthInstitution { HealthInstitutionId = 6, FacilityTypeId = 3, HealthInstitutionName = "مكتب2" }
            );
        }
    }
}
