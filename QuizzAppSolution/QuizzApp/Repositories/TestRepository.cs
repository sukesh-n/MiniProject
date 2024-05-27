using QuizzApp.Interfaces;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class TestRepository : IRepository<int, Test>
    {
        public Task<Test> AddAsync(Test entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Models.DTO.Option option)
        {
            throw new NotImplementedException();
        }

        public Task<Test> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Test>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Test> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<Test> UpdateAsync(Test entity)
        {
            throw new NotImplementedException();
        }
    }
}
