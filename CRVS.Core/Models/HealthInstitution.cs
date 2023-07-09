using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRVS.Core.Models
{
    public class HealthInstitution
    {
        public int HealthInstitutionId { get; set; }
        public string? HealthInstitutionName { get; set; }
        public int GovernorateId { get; set; }
        public int DohId { get; set; }
        public int FacilityTypeId { get; set; }
    }
}
