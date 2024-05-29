using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces
{
    public interface IQuestionService
    {
        public Task<bool> AddQuestionWithAnswerAsync(QuestionSolutionDTO questionSolutionDTO);
        public Task<QuestionSolutionDTO> UpdateQuestionsWithSolution(QuestionSolutionDTO questionSolutionDTO);

        public Task<List<Question>> GetQuestionWithCategory(QuestionSelectionDTO questionSelectionDTO);
        public Task<List<Category>> GetAllCategoriesAsync();
    }
}
