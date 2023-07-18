using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models.ViewModels
{
    public class ChangePassViewModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "كلمة السر الجديدة غير متطابقة")]
        public string? ConfirmNewPassword { get; set; }
    }
}
