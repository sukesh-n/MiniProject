using QuizzApp.Context;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class SolutionRepository : IRepository<int, Solution>
    {
        private readonly QuizzAppContext _context;

        public SolutionRepository(QuizzAppContext context)
        {
            _context = context;
        }

        public async Task<Solution> AddAsync(Solution entity)
        {
            try
            {
                var Duplicate = _context.solutions.FirstOrDefault(s => s.QuestionId == entity.QuestionId);
                if(Duplicate != null)
                {
                   return Duplicate;
                }
                var result = await _context.solutions.AddAsync(entity);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch
            {
                throw new ErrorInConnectingRepository("Unable to add Solution to Database");
            }
        }

        public Task<Solution> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Solution>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Solution> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<Solution> UpdateAsync(Solution entity)
        {
            throw new NotImplementedException();
        }
    }
}
