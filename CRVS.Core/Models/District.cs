using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models
{
    public class District
    {
        public int DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public int JudiciaryId { get; set; }
    }
}
