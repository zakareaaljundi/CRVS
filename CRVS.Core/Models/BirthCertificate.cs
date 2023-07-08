using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRVS.Core.Models
{
    public class BirthCertificate
    {
        public int BirthCertificateId { get; set; }
        [Required]
        [Display(Name = "Child Name")]
        public string? ChildName { get; set; }
        [Display(Name = "Gender")]
        public Venus Venu { get; set; }
        public enum Venus
        {
            Male, Female
        }
        public string? Governorate { get; set; }
        public string? Directorate { get; set; }
        public string? Judiciary { get; set; }
        public string? District { get; set; }
        public string? Village { get; set; }
        public string? FacilityType { get; set; }
        public string? HealthInstitution { get; set; }
        public string? FatherName { get; set; }
        public string? FatherJob { get; set; }
        public string? FatherReligion { get; set; }
        public string? MotherName { get; set; }
        public string? MotherJob { get; set; }
        public string? MotherReligion { get; set; }
        public bool IsDeleted { get; set; }
        public bool FirstStage { get; set; }
        public bool SecondStage { get; set; }
        public bool Approval { get; set; }
        public string? Creator { get; set; }
    }
}
