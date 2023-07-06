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
    public class Directorate
    {
        public int DirectorateId { get; set; }
        public string? DirectorateName { get; set; }
        public int GovernorateId { get; set; }
    }
}
