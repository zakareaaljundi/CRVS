using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRVS.Core.Models.ViewModels
{
    public class BirthCertificateViewModel
    {
        public int BirthCertificateId { get; set; }
        public string? HealthId { get; set; }
        public string? ChildName { get; set; }
        public Genders Gender { get; set; }
        public enum Genders
        {
            ذكر, أنثى, خنثى
        }
        public string? Governorate { get; set; }
        public string? Doh { get; set; }
        public string? District { get; set; }
        public string? Nahia { get; set; }
        public string? Village { get; set; }
        public string? FacilityType { get; set; }
        public string? HealthInstitution { get; set; }
        public BirthTypes BirthType { get; set; }
        public enum BirthTypes
        {
            طبيعية, قيصرية
        }
        public NumberOfBirths NumberOfBirth { get; set; }
        public enum NumberOfBirths
        {
            مفردة, ثنائية, ثلاثية, أكثر
        }
        [DataType(DataType.Time)]
        public DateTime? BirthHour { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        public string? DOBText { get; set; }
        public string? FatherFName { get; set; }
        public string? FatherMName { get; set; }
        public string? FatherLName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        public DateTime? FatherDOB { get; set; }
        public string? FatherAge { get; set; }
        public int? FatherJobId { get; set; }
        public int? FatherNationalityId { get; set; }
        public int? FatherReligionId { get; set; }
        public string? FatherMobile { get; set; }
        public string? MotherFName { get; set; }
        public string? MotherMName { get; set; }
        public string? MotherLName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        public DateTime? MotherDOB { get; set; }
        public string? MotherAge { get; set; }
        public int? MotherJobId { get; set; }
        public int? MotherNationalityId { get; set; }
        public int? MotherReligionId { get; set; }
        public string? MotherMobile { get; set; }
        public Relatives Relative { get; set; }
        public enum Relatives
        {
            يوجد, لا_يوجد
        }
        public int? Alive { get; set; }
        public int? BornAliveThenDied { get; set; }
        public int? StillBirth { get; set; }
        public int? BornDisable { get; set; }
        public int? NoAbortion { get; set; }
        public IsDisableds IsDisabled { get; set; }
        public enum IsDisableds
        {
            نعم, لا
        }
        public int? DisabledTypeId { get; set; }
        public int? DurationOfPregnancy { get; set; }
        public decimal? BabyWeight { get; set; }
        public string? PlaceOfBirth { get; set; }
        public BirthOccurredBys BirthOccurredBy { get; set; }
        public enum BirthOccurredBys
        {
            طبيب, ممرضة, قابلة, أخرى
        }
        public string? KabilaName { get; set; }
        public string? LicenseNo { get; set; }
        public DateTime? LicenseYear { get; set; }
        public int? FamilyGovernorateId { get; set; }
        public int? FamilyDistrictId { get; set; }
        public int? FamilyNahiaId { get; set; }
        public string? FamilyMahala { get; set; }
        public string? FamilyDOH { get; set; }
        public string? FamilySector { get; set; }
        public string? FamilyPHC { get; set; }
        public string? FamilyZigag { get; set; }
        public string? FamilyHomeNo { get; set; }
        public DocumentTypes DocumentType { get; set; }
        public enum DocumentTypes
        {
            هوية_أحوال, بطاقة_موحدة, جواز_سفر
        }
        public string? RecordNumber { get; set; }
        public string? PageNumber { get; set; }
        public string? CivilStatusDirectorate { get; set; }
        public string? GovernorateCivilStatusDirectorate { get; set; }
        public NationalIdFors NationalIdFor { get; set; }
        public enum NationalIdFors
        {
            أب, أم
        }
        public int? NationalId { get; set; }
        public string? PassportNo { get; set; }
        public string? InformerName { get; set; }
        public string? InformerJobTitle { get; set; }
        public string? KinshipOfTheNewborn { get; set; }
        public string? BirthPerformerName { get; set; }
        public string? BirthPerformerWorkingAddress { get; set; }
        public string? HospitalManagerName { get; set; }
        public string? HospitalManagerSig { get; set; }
        public string? RationCard { get; set; }
        public IFormFile? ImageBirthCertificate { get; set; }
        public IFormFile? ImageMarriageCertificate { get; set; }
        public IFormFile? ImageFatherUnifiedNationalIdFront { get; set; }
        public IFormFile? ImageFatherUnifiedNationalIdBack { get; set; }
        public IFormFile? ImageMotherUnifiedNationalIdFront { get; set; }
        public IFormFile? ImageMotherUnifiedNationalIdBack { get; set; }
        public IFormFile? ImageResidencyCardFront { get; set; }
        public IFormFile? ImageResidencyCardBack { get; set; }
        public string? SaveBtn { get; set; }
        public bool BiostatisticsStage { get; set; }
        public bool Approval { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRejected { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? Creator { get; set; }
    }
}
