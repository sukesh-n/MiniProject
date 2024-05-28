using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces.Test
{
    public interface ITestInterface
    {
        public Task<TestDTO> ChooseQuestion(QuestionSelectionDTO questionSelectionDTO); 
    }
}
