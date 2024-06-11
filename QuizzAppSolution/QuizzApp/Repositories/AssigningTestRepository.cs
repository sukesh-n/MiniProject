using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuizzApp.Repositories
{
    public class AssigningTestRepository : IAssignedTestRepository
    {
        private readonly QuizzAppContext _context;

        public AssigningTestRepository(QuizzAppContext context)
        {
            _context = context;
        }

        public async  Task<AssignedTest> AddAsync(AssignedTest entity)
        {
            try
            {
                var result = await _context.assignedTests.AddAsync(entity);
                await _context.SaveChangesAsync();
                if (result == null)
                {
                    throw new Exception();
                }
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository("Unable to connect repository");
            }
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

        public async Task<AssignedTest> GetByAssignemntNo(int AssignmentNo)
        {
            try
            {
                var result = await _context.assignedTests.FirstOrDefaultAsync(e => e.AssignmentNo == AssignmentNo);
                return result;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository("Unable to connect repository");
            }
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
