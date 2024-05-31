namespace QuizzApp.Models.DTO
{
    public class QuestionSolutionDTO
    {
        public int QuestionId { get; set; }
        public string QuestionDescription { get; set; } = string.Empty;
        public string MainCategory { get; set; } = string.Empty;
        public string SubCategory { get; set; } = string.Empty;
        public string QuestionType { get; set; } = string.Empty;
        public double? NumericalAnswer { get; set; } = null;
        public bool? TrueFalseAnswer { get; set; } = null;
        public int QuestionDifficultyLevel { get; set; }
        public List<string> Options { get; set; } = new List<string>();

        public string CorrectOptionAnswer { get; set; } = string.Empty;

    }
    //public class OptionDTO
    //{
    //    public List<string> Options { get; set; }
    //}
}
