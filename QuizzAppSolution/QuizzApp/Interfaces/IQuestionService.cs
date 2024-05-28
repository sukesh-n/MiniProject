using QuizzApp.Models.DTO;

namespace QuizzApp.Interfaces
{
    public interface IQuestionService
    {
        public Task<bool> AddQuestionWithAnswerAsync(QuestionSolutionDTO questionSolutionDTO);
        public Task<QuestionSolutionDTO> UpdateQuestionsWithSolution(QuestionSolutionDTO questionSolutionDTO);

        public Task<QuestionWithCategoryDTO> GetQuestionWithCategory(QuestionWithCategoryDTO questionWithCategoryDTO);
    }
}
