using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GR.MVC.Models
{
    public class SingInViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(26)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
