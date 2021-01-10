using System.ComponentModel.DataAnnotations;

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
