using QuizzApp.Interfaces;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class ResultRepository : IRepository<int, Result>
    {
        public Task<Result> AddAsync(Result entity)
        {
            throw new NotImplementedException();
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
