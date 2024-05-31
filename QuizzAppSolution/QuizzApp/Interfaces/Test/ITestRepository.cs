using QuizzApp.Models;

namespace QuizzApp.Interfaces.Test
{
    public interface ITestRepository : IRepository<int,TestDTO>
    {
        public Task<List<TestDTO>> AddTestListAsync(List<TestDTO> testDTOs);
    }
}
