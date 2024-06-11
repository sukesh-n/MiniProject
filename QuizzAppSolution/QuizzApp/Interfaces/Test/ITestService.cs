using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces.Test
{
    public interface ITestService
    {
        public Task<TestAssign> ChooseQuestion(QuestionSelectionDTO questionSelectionDTO); 
        public Task<NotificationDTO> AssignTest(TestAssign testDTO);
        public Task<List<TestDTO>> PublishTest(List<TestDTO> testDTO);
        public Task<List<QuestionDTO>> GetTestQuestions(int AssignmentNumber, string email);
        public Task<List<QuestionDTO>> AttendTest(List<QuestionDTO> questionDTO, int AssignmentNumber, string email);
    }
}
