using QuizzApp.Models;

namespace QuizzApp.Interfaces.Test
{
    public interface ITestRepository : IRepository<int,TestDTO>
    {
        
        public Task<List<TestDTO>> AddTestListAsync(List<TestDTO> testDTOs);
        public Task<TestDTO> GetTestByAssignmentNoAsync(int AssignmentNo);
        public Task<TestDTO> GetTestByUserIdAndAssignemtnNumber(int UserId,int AssignmentNumber);

    }
}
