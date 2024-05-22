using QuizzApp.Interfaces;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class QuestionRepository : IRepository<int, Question>
    {
        public Task<Question> AddAsync(Question entity)
        {
            throw new NotImplementedException();
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
