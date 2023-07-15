using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models
{
    public class Governorate
    {
        [Key]
        public int GovernorateId { get; set; }
        public string? GovernorateName { get; set; }
        public bool IsArabian { get; set; }
    }
}
