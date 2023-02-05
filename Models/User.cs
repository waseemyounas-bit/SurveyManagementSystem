using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="Minimum password length should be 6 characters. ")]
        public string? Password { get; set; }
        public int RoleId { get; set; } = 2;
        public int IsActive { get; set; } = 0;
        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm password must be same.")]
        public string? ConfirmPassword { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [NotMapped]
        public List<SelectListItem> roles { get; set; } = new List<SelectListItem>() { 
        new SelectListItem(){Value="1",Text="Admin"},
        new SelectListItem(){Value="2",Text="Manager"}
        };
    }
}