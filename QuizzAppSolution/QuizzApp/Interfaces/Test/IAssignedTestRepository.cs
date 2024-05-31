using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces.Test
{
    public interface IAssignedTestRepository : IRepository<int,TestAssignDTO>
    {
        public Task<AssignedTest> GetUserByEmailAsync(string email);
        
    }
}
