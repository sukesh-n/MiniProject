using QuizzApp.Models;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces.Test
{
    public interface IAssignedTestRepository : IRepository<int,AssignedTest>
    {
        public Task<AssignedTest> GetUserByEmailAsync(string email);
        
    }
}
