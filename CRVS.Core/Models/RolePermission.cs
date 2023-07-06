using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string? RoleId { get; set; }
        public string? TableName { get; set; }
        public bool ReadPermission { get; set; }
        public bool AddPermission { get; set; }
        public bool EditPermission { get; set; }
        public bool DeletePermission { get; set; }
        public bool ApprovalPermission { get; set; }
    }
}
