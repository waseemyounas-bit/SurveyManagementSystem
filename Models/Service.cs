using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Service
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Service name must be greater than 10 characters.")]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        [Required(ErrorMessage ="Please select a company.")]
        public Guid? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
    }
}
