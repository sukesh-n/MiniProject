using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces.Test
{
    public interface IAssignedTestRepository : IRepository<int,AssignedTest>
    {
        public Task<AssignedTest> GetByAssignemntNo(int AssignmentNo);
        public Task<List<AssignedTestDTO>> GetAllTestsByOrganizer(int UserId);

        public Task<List<AssignedTest>> GetTestDetails(int assignmentNo);
    }
}
