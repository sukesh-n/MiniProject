using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Exceptions;
using QuizzApp.Context;
using QuizzApp.Interfaces.ResultInterface;

namespace QuizzApp.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly QuizzAppContext _context;

        public ResultRepository(QuizzAppContext context)
        {
            _context = context;
        }

        public async Task<Result> AddAsync(Result entity)
        {
            try
            {
                var result = await _context.results.AddAsync(entity);
                await _context.SaveChangesAsync();
                if (result != null)
                {
                    return result.Entity;
                }
                else
                {
                    throw new ErrorInConnectingRepository("Unable to add results");
                }

            }
            catch
            {
                throw new ErrorInConnectingRepository();
            }
        }

        public Task<Result> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Result>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateAsync(Result entity)
        {
            throw new NotImplementedException();
        }
    }
}
