namespace QuizzApp.Models.DTO
{
    public class QuestionWithCategoryDTO : Question
    {
        public string MainCategory { get; set; } = string.Empty;
        public string? SubCategory {get; set; } = string.Empty;
    }
}
