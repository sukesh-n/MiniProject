using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Exceptions;

namespace QuizzApp.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly QuizzAppContext _context;

        public TestRepository(QuizzAppContext context)
        {
            _context = context;
        }
        public Task<TestDTO> AddAsync(TestDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TestDTO>> AddTestListAsync(List<TestDTO> testDTOs)
        {
            try
            {
                var result = _context.tests.AddRangeAsync(testDTOs);
                await _context.SaveChangesAsync();
                return testDTOs;
            }
            catch (Exception ex) {
                throw new ErrorInConnectingRepository(ex.Message);
            }
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
