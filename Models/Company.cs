using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Company
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3)]
        public string? Title { get; set; }
        public string? CRN { get; set; } = "";
        [Required]
        public string? Address { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email")]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        [MinLength(10,ErrorMessage ="Please add more than 10 characters.")]
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        public IQueryable<Service>? Services { get; set; }
    }
}
