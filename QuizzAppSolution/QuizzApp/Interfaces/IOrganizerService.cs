using QuizzApp.Models.DTO.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO;

namespace QuizzApp.Interfaces
{
    public interface IOrganizerService
    {
        public Task<List<TestAssignDTO>> AssignTest(TestAssignDTO testAssignDTO, QuestionSelectionDTO questionSelectionDTO);
        public Task<List<QuestionSolutionDTO>> GenerateQuizzApiWithSolution(QuestionSelectionDTO questionSelectionDTO);
        
    }
}
