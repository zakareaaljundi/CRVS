using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
