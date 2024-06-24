using QuizzApp.Repositories;
using QuizzApp.Models;
namespace QuizzApp.Interfaces.Solutions
{
    public interface IOptionsRepository : IRepository<int, Option>
    {
        public Task<List<Option>> GetAllByQuestionIdAsync(List<int> QuestionIds);
    }
}
