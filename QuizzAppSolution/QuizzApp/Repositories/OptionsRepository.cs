using QuizzApp.Interfaces;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class OptionsRepository : IRepository<int, Option>
    {
        public Task<Option> AddAsync(Option entity)
        {
            throw new NotImplementedException();
        }

        public Task<Option> AddAsync(Models.DTO.Option option)
        {
            throw new NotImplementedException();
        }

        public Task<Option> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Option>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Option> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<Option> UpdateAsync(Option entity)
        {
            throw new NotImplementedException();
        }
    }
}
