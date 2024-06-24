using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Solutions;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class OptionsRepository : IOptionsRepository
    {
        private readonly QuizzAppContext _context;

        public OptionsRepository(QuizzAppContext context)
        {
            _context = context;
        }

        public async Task<Option> AddAsync(Option entity)
        {
            try
            {
                var duplicate = _context.options.FirstOrDefault(o => o.QuestionId == entity.QuestionId && o.Value == entity.Value);

                if (duplicate == null)
                {
                    _context.options.Add(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Option> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Option>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Option>> GetAllByQuestionIdAsync(List<int> QuestionIds)
        {
            try
            {
                List<Option> allOptions = new List<Option>();

                foreach (int questionId in QuestionIds)
                {
                    var optionsForQuestion = _context.options.Where(o => o.QuestionId == questionId).ToList();
                    allOptions.AddRange(optionsForQuestion);
                }

                return Task.FromResult(allOptions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
