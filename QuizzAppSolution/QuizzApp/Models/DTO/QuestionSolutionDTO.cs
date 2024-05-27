namespace QuizzApp.Models.DTO
{
    public class QuestionSolutionDTO
    {
        public string QuestionDescription { get; set; } = string.Empty;
        public string MainCategory { get; set; } = string.Empty;
        public string SubCategory { get; set; } = string.Empty;
        public double? NumericalAnswer { get; set; }
        public bool? TrueFalseAnswer { get; set; }
        public int QuestionDifficultyLevel { get; set; }
        public Option Option { get; set; }
        public string CorrectOption { get; set; }
        public QuestionSolutionDTO()
        {
            Option = new Option();
        }
    }

    public class Option
    {
        public string OptionA { get; set; } = string.Empty;
        public string OptionB { get; set; } = string.Empty;
        public string OptionC { get; set; } = string.Empty;
        public string OptionD { get; set; } = string.Empty;
    }
}
