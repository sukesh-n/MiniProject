using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;
namespace QuizzApp.Interfaces.Test
{
    public interface IAssignedTestEmailRepository : IRepository<int,AssignedTestEmail>
    {
        public Task<List<AssignedTestEmail>> GetByUserEmailAsync(string userEmail);
        
        public Task<bool> AddEmailsForTest(int AssignmentNo, List<AssignedTestEmailDTO> assignedTestEmailDTO);
        public Task<bool> UpdateDb(List<AssignedTestEmail> assignedTestEmails);
    }
}
