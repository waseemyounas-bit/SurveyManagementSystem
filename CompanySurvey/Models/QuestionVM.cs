using Models;
using System.ComponentModel.DataAnnotations;

namespace CompanySurvey.Models
{
    public class QuestionVM
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Question Text must be greater than 10 characters.")]
        public string? Text { get; set; }
        [Required]
        public string? OPA { get; set; } = "N/A";
        [Required]
        public string? OPB { get; set; } = "N/A";
        [Required]
        public string? OPC { get; set; } = "N/A";
        [Required]
        public string? OPD { get; set; } = "N/A";
        [Required]
        public int Limit { get; set; } = 0;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool IsMandatory { get; set; } = false;
        public Guid? QuestionTypeId { get; set; }
        public Guid? SurveyId { get; set; }
        public virtual string? SurveyName { get; set; }
    }
}
