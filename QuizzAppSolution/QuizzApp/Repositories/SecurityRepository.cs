using QuizzApp.Interfaces;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class SecurityRepository : IRepository<int, Security>
    {
        public Task<Security> AddAsync(Security entity)
        {
            throw new NotImplementedException();
        }

        public Task<Security> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Security>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Security> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<Security> UpdateAsync(Security entity)
        {
            throw new NotImplementedException();
        }
    }
}
