using QuizzApp.Context;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Exceptions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<AssignedTestEmail>> GetAllAsync()
        {
            try
            {
                var result = await _context.assignedTestEmails.ToListAsync();
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch
            {
                throw new ErrorInConnectingRepository();
            }
        }

        public Task<AssignedTestEmail> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AssignedTestEmail>> GetByUserEmailAsync(string userEmail)
        {
            try
            {
                var result = await _context.assignedTestEmails.Where(e => e.Email == userEmail).ToListAsync();

                return result;
            }
            catch
            {
                throw new ErrorInConnectingRepository();
            }
        }

        public async Task<List<AssignedTestEmail>> GetEmailsAsync(int assignmentNo)
        {
            try
            {
                var result = await _context.assignedTestEmails.Where(e => e.AssignmentNumber == assignmentNo).ToListAsync();
                if (result == null)
                {
                    throw new EmptyRepositoryException();
                }
                return result;
            }
            catch
            {
                throw new ErrorInConnectingRepository();
            }
        }

        public Task<AssignedTestEmail> UpdateAsync(AssignedTestEmail entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateDb(List<AssignedTestEmail> assignedTestEmails)
        {
            try
            {
                _context.assignedTestEmails.UpdateRange(assignedTestEmails);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateDb: {ex.Message}");
                return false;
            }
        }

    }
}
