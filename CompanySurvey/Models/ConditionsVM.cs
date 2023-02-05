namespace CompanySurvey.Models
{
    public class ConditionsVM
    {
        public Guid? QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string State { get; set; }
        public string Value { get; set; }
        public string Do { get; set; }
        public string? QuestionIds { get; set; }
        public string ShowHideQuestionsText { get; set; }

        public Guid? SurveyId { get; set; }
        public Guid? ConditionId { get; set; }
    }
}
