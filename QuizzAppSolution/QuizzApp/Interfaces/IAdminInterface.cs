using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.AnalyseServiceDTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces
{
    public interface IAdminInterface : ICommonQuestionInterface
    {
        Task<bool> AddQuestionWithAnswerAsync(QuestionSolutionDTO questionSolutionDTO);
        Task<TestDTO> ConductCommonQuizAsync(List<User> candidates, QuestionSelectionDTO questionSelectionDTO);
        Task<QuestionSolutionDTO> UpdateQuestionsWithSolution(QuestionSolutionDTO questionSolutionDTO);
        Task<ResultDTO> ViewResultAnalysis();
    }
}
