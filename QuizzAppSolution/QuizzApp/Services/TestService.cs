using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public Task<NotificationDTO> AssignTest(TestAssign testDTO)
        {
            throw new NotImplementedException();
        }

        public Task<TestAssign> ChooseQuestion(QuestionSelectionDTO questionSelectionDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TestDTO>> PublishTest(List<TestDTO> testDTO)
        {
            try
            {
                var TestPublish = await _testRepository.AddTestListAsync(testDTO);
                return TestPublish;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
