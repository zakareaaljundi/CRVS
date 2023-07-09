using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models
{
    public class Nahia
    {
        public int NahiaId { get; set; }
        public string? NahiaName { get; set; }
        public int GovernorateId { get; set; }
        public int DohId { get; set; }
        public int DistrictId { get; set; }
    }
}
