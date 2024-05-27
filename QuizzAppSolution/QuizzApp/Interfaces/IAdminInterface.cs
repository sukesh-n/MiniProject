using QuizzApp.Models;
using QuizzApp.Models.DTO;

namespace QuizzApp.Interfaces
{
    public interface IAdminInterface
    {
        Task<bool> AddQuestionWithAnswerAsync();
        Task<TestDTO> ConductCommonQuizAsync(List<User> candidates, Test quiz);
    }
}
