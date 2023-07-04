using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter First Name")]
        [MinLength(3, ErrorMessage = "First Name must be at least 3 characters")]
        [MaxLength(13, ErrorMessage = "First Name cannot exceed 13 characters")]
        public string? FName { get; set; }

        [Required(ErrorMessage = "Enter Last Name")]
        [MinLength(3, ErrorMessage = "Last Name must be at least 3 characters")]
        [MaxLength(13, ErrorMessage = "Last Name cannot exceed 13 characters")]
        public string? LName { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm And Password Not Match")]
        public string? ConfirmPassword { get; set; }
        public string? Phone { get; set; }
        public IFormFile? Img { get; set; }
    }
}
