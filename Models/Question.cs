using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Question
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(10, ErrorMessage ="Question Text must be greater than 10 characters.")]
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
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        public bool IsMandatory { get; set; } = false;
        public Guid? QuestionTypeId { get; set; }
        public virtual QuestionType? QuestionType { get; set; }
        public virtual IQueryable<Answer>? Answers { get; set; }
        public Guid? SurveyId { get; set; }
        public virtual Survey? Survey { get; set;}
        [NotMapped]
        public List<SelectListItem>? scales { get; set; } =
            new List<SelectListItem>() { new SelectListItem() {Value="1", Text="Scale from 1-5" },
                new SelectListItem() {Value="2", Text="Scale from 1-7" },
                new SelectListItem() {Value="3", Text="Scale from 1-10" } };
        [NotMapped]
        public string? CompanyName { get; set; }
        [NotMapped]
        public string? ServiceName { get; set; }
        [NotMapped]
        public string? Answer { get; set; }
        [NotMapped]
        public string? Type { get; set; }
    }
}
