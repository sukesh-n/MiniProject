namespace QuizzApp.Models.DTO.Test
{
    public class ScoreDTO
    {
        public int TestId { get; set; }
        public int UserId { get; set; }
        public int AssignmentNumber { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalCorrectAnswers { get; set; }
        public int TotalWrongAnswers { get; set; }
        public int Score { get; set; }
    }
}
