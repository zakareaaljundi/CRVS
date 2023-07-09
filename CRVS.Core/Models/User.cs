using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models
{
    public class User
    {
        public string? UserId { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Img { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string? Governorate { get; set; }
        public string? Directorate { get; set; }
        public string? District { get; set; }
        public string? Judiciary { get; set; }
        public string? Village { get; set; }
        public string? FacilityType { get; set; }
        public string? HealthInstitution { get; set; }
    }
}
