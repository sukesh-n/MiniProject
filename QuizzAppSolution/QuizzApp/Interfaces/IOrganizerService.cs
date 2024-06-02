using QuizzApp.Models.DTO.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO;

namespace QuizzApp.Interfaces
{
    public interface IOrganizerService
    {
        public Task<TestAssign> AssignTest(TestAssign testAssignDTO, QuestionSelectionDTO questionSelectionDTO);
        public Task<List<QuestionSolutionDTO>> GenerateQuizzApiWithSolution(QuestionSelectionDTO questionSelectionDTO);
        
    }
}
