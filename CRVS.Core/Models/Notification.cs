using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string? HeadLine { get; set; }
        public string? Description { get; set; }
        public DateTime DAT { get; set; }
        public bool IsRead { get; set; }
        public bool IsGoodFeedBack { get; set; }
        public bool IsSettingMessage { get; set; }
        public string? CurrentUser { get; set; }
        public int CertificateId { get; set; }
    }
}
