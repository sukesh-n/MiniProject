using QuizzApp.Models;
using QuizzApp.Models.DTO;

namespace QuizzApp.Interfaces.Test
{
    public interface IAssignedQuestionRepository : IRepository<int,AssignedQuestions>
    {
        public Task<List<AssignedQuestions>> AddQuestionsForTest(int AssignmentNo,List<QuestionSolutionDTO> GetQuestionsWithSolution);
        public Task<List<int>> GetQuestionByAssignmentNumber(int AssignmentNumber);
        
    }
}
