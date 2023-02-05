using System.ComponentModel.DataAnnotations;

namespace CompanySurvey.Models
{
    public class SurveyFilterDTO
    {
        [Required]
        public Guid? CompanyId { get; set; }
        [Required]
        public Guid? ServiceId { get; set; }
    }
}
