using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Survey
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(10,ErrorMessage ="Description length must be atleast 10 characters long.")]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        [NotMapped]
        public Guid? CompanyId { get; set; }
        public Guid? ServiceId { get; set; }
        public virtual Service? Service { get; set; }
        public IQueryable<Question>? Questions { get; set; }
        public IQueryable<Condition>? Conditions { get; set; }
        [NotMapped]
        public Guid? CustomerId { get; set; }
    }
}
