using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.AnalyseServiceDTO;

namespace QuizzApp.Interfaces
{
    public interface IAdminInterface : ICommonQuestionInterface
    {
        Task<bool> AddQuestionWithAnswerAsync(QuestionSolutionDTO questionSolutionDTO);
        Task<TestDTO> ConductCommonQuizAsync(List<User> candidates, Test quiz);
        Task<QuestionSolutionDTO> UpdateQuestionsWithSolution(QuestionSolutionDTO questionSolutionDTO);
        Task<ResultDTO> ViewResultAnalysis();
    }
}
