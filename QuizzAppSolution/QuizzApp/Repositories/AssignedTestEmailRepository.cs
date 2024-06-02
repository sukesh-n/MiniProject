using QuizzApp.Context;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Exceptions;

namespace QuizzApp.Repositories
{
    public class AssignedTestEmailRepository : IAssignedTestEmailRepository
    {
        private readonly QuizzAppContext _context;

        public AssignedTestEmailRepository(QuizzAppContext context)
        {
            _context = context;
        }

        public Task<AssignedTestEmail> AddAsync(AssignedTestEmail entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddEmailsForTest(int AssignmentNo, List<AssignedTestEmailDTO> assignedTestEmailDTO)
        {
            try
            {
                var status = false;
                foreach (var email in assignedTestEmailDTO)
                {
                    var CheckDuplicate = _context.assignedTestEmails.FirstOrDefault(e => e.Email == email.Email && e.AssignmentNumber == email.AssignmentNumber);
                    if (CheckDuplicate == null)
                    {

                        var addDetail = await _context.assignedTestEmails.AddAsync(email);
                        await _context.SaveChangesAsync();
                        if (addDetail != null)
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                    }

                }
                return status;
                
            }
            catch
            {
                throw new ErrorInConnectingRepository();
            }
        }

        public Task<AssignedTestEmail> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssignedTestEmail>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AssignedTestEmail> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<AssignedTestEmail> UpdateAsync(AssignedTestEmail entity)
        {
            throw new NotImplementedException();
        }
    }
}
