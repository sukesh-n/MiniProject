using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Exceptions;
namespace QuizzApp.Repositories
{
    public class QuestionRepository : IRepository<int, Question>
    {
        private readonly QuizzAppContext _context;

        public QuestionRepository(QuizzAppContext context)
        {
            _context = context;
        }

        public async Task<Question> AddAsync(Question entity)
        {
            try
            {
                var DuplicateCheck = _context.questions.FirstOrDefault(q => q.QuestionDescription == entity.QuestionDescription);
                if(DuplicateCheck == null)
                {
                    var result = await _context.questions.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return result.Entity;
                }
                return DuplicateCheck;
            }
            catch
            {
                throw new ErrorInConnectingRepository("Unable to add Question");
            }
        }

        public Task<Question> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Question>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Question> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<Question> UpdateAsync(Question entity)
        {
            throw new NotImplementedException();
        }
    }
}
