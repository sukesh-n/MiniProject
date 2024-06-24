using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Exceptions;
using QuizzApp.Context;
using QuizzApp.Interfaces.ResultInterface;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Result>> GetAllAsync()
        {
            try
            {
                var results = await _context.results.ToListAsync();
                if (results != null)
                {
                    return results;
                }
                else
                {
                    throw new ErrorInConnectingRepository("Results not found");
                }
            }
            catch
            {
                throw new ErrorInConnectingRepository();
            }
        }

        public async Task<Result> GetAsync(int ResultId)
        {
            try
            {
                var result = await _context.results.FindAsync(ResultId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new ErrorInConnectingRepository("Result not found");
                }
            }
            catch
            {
                throw new ErrorInConnectingRepository();
            }
        }

        public Task<Result> UpdateAsync(Result entity)
        {
            throw new NotImplementedException();
        }
    }
}
