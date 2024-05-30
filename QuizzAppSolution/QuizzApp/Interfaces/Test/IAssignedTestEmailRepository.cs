using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;
namespace QuizzApp.Interfaces.Test
{
    public interface IAssignedTestEmailRepository : IRepository<int,AssignedTestEmail>
    {
        public Task<bool> AddEmailsForTest(int AssignmentNo, List<AssignedTestEmailDTO> assignedTestEmailDTO);
    }
}
