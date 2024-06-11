using Microsoft.EntityFrameworkCore;
using QuizzApp.Context;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces.Solutions;
using QuizzApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzApp.Repositories
{
    public class SolutionRepository : ISolutionRepository
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

        public async Task<List<Solution>> GetSolutionForQuestions(List<int> QuestionIds)
        {
            List<Solution> solutions = new List<Solution>();
            try
            {
                foreach (var questionId in QuestionIds)
                {
                    var solution = await _context.solutions.FirstOrDefaultAsync(s => s.QuestionId == questionId);
                    if (solution != null)
                    {
                        solutions.Add(solution);
                    }
                }
                return solutions;
            }
            catch
            {
                throw new ErrorInConnectingRepository("Unable to fetch Solutions");
            }
        }

        public async Task<List<Solution>> GetSolutions(List<int> questionIds)
        {
            try
            {
                return await _context.solutions
                    .Where(solution => questionIds.Contains(solution.QuestionId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while fetching solutions", ex);
            }
        }

        public Task<Solution> UpdateAsync(Solution entity)
        {
            throw new NotImplementedException();
        }
    }
}
