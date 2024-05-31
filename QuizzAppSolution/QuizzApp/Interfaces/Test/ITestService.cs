using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces.Test
{
    public interface ITestService
    {
        public Task<TestAssignDTO> ChooseQuestion(QuestionSelectionDTO questionSelectionDTO); 
        public Task<NotificationDTO> AssignTest(TestAssignDTO testDTO);
        public Task<List<TestDTO>> PublishTest(List<TestDTO> testDTO);
    }
}
