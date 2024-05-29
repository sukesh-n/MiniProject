using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class AssigningTestRepository : IAssignedTestRepository
    {
        public Task<AssignedTest> AddAsync(AssignedTest entity)
        {
            throw new NotImplementedException();
        }

        public Task<AssignedTest> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssignedTest>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AssignedTest> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<AssignedTest> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AssignedTest> UpdateAsync(AssignedTest entity)
        {
            throw new NotImplementedException();
        }
    }
}
