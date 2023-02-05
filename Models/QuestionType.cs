using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class QuestionType
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Question type must be greater than 10 characters.")]
        public string? Type { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Question type description must be greater than 10 characters.")]
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        public List<Question>? Questions { get; set;}
    }
}
