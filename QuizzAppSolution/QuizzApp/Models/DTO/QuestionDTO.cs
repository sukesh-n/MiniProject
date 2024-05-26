namespace QuizzApp.Models.DTO
{
    public class QuestionDTO : Question
    {
        public int? OptionId { get; set; }
        public double? NumericalAnswer { get; set; }
        public bool? TrueFalseAnswer { get; set; }
    }
}
