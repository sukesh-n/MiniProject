using QuizzApp.Interfaces;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class SolutionRepository : IRepository<int, Solution>
    {
        public Task<Solution> AddAsync(Solution entity)
        {
            throw new NotImplementedException();
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
