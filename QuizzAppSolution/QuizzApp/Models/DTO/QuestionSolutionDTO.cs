namespace QuizzApp.Models.DTO
{
    public class QuestionSolutionDTO
    {
        public string QuestionDescription { get; set; } = string.Empty;
        public string MainCategory { get; set; } = string.Empty;
        public string SubCategory { get; set; } = string.Empty;
        public string QuestionType { get; set; } = string.Empty;
        public double? NumericalAnswer { get; set; }
        public bool? TrueFalseAnswer { get; set; }
        public int QuestionDifficultyLevel { get; set; }
        public OptionDTO? Option { get; set; }
        public string CorrectAnswer { get; set; } = string.Empty;
    }
}
