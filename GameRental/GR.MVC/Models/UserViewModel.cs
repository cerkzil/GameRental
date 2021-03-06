﻿using System.ComponentModel.DataAnnotations;

namespace GR.MVC.Models
{
    public class RegistrationViewModel
        {
            [Required]
            [StringLength(26, ErrorMessage = "The {0} must be atleast {2} and at max {1} characters long.", MinimumLength = 6)]
            public string Name { get; set; }

            [Required(ErrorMessage = "The email address is required")]
            [Display(Name = "Email Address")]
            [EmailAddress(ErrorMessage = "Invalid email address.")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            [StringLength(26, ErrorMessage = "The {0} must be atleast {2} and at max {1} characters long.", MinimumLength = 6)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do no match.")]
            public string ConfirmPassword { get; set; }
        }
}