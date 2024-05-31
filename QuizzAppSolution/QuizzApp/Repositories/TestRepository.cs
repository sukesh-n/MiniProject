using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class TestRepository : ITestRepository
    {
        public Task<TestDTO> AddAsync(TestDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<TestDTO>> AddTestListAsync(List<TestDTO> testDTOs)
        {
            throw new NotImplementedException();
        }

        public Task<TestDTO> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TestDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TestDTO> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<TestDTO> UpdateAsync(TestDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
