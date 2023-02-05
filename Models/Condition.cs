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
    public class Condition
    {
        public Guid Id { get; set; }
        [Required]
        public Guid? QuestionId { get; set; }
        public Guid? SurveyId { get; set; }
        public virtual Survey? Survey { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? Value { get; set; }
        [Required]
        public string? Do { get; set; }
        [Required]
        public string? QuestionIds { get; set; }
        [NotMapped]
        public List<SelectListItem>? Questions { get; set; }
        [NotMapped]
        public List<SelectListItem> States { get; set; } = new List<SelectListItem>() { 
        new SelectListItem{Text="Is Equal" ,Value="1"},
        new SelectListItem{Text="Is Not Equal" ,Value="2"},
        new SelectListItem{Text="Is Empty" ,Value="3"},
        new SelectListItem{Text="Is Fill" ,Value="4"}
        };
        [NotMapped]
        public List<SelectListItem> DoList { get; set; } = new List<SelectListItem>() { 
        new SelectListItem{Text="Show" ,Value="1"},
        new SelectListItem{Text="Hide" ,Value="2"}
        };
    }
}
