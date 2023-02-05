using System.ComponentModel.DataAnnotations;

namespace CompanySurvey.Models
{
    public class LoginViewModel
    {
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email")]
        public string? Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "password should be between 8 and 50 characters")]
        public string? Password { get; set; }
    }
}
