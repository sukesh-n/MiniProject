using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO;

namespace QuizzApp.Repositories
{
    public class AssigningTestRepository : IAssignedTestRepository
    {
        public Task<TestAssignDTO> AddAsync(TestAssignDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<TestAssignDTO> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TestAssignDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TestAssignDTO> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<AssignedTest> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<TestAssignDTO> UpdateAsync(TestAssignDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
