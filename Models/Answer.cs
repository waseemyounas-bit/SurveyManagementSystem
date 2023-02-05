using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string? Value { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Guid? QuestionId { get; set; }
        public virtual Question? Question { get; set; }
        public Guid? SurveyId { get; set; }
        public virtual Survey? Survey { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
