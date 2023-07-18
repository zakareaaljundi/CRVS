using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models.ViewModels
{
    public class UpdateProfileViewModel
    {
        public string? UserId { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? OldImg { get; set; }
        public IFormFile? Img { get; set; }
        public string? Governorate { get; set; }
        public string? Doh { get; set; }
        public string? District { get; set; }
        public string? Nahia { get; set; }
        public string? Village { get; set; }
        public string? FacilityType { get; set; }
        public string? HealthInstitution { get; set; }
        public string? RoleName { get; set; }
    }
}
