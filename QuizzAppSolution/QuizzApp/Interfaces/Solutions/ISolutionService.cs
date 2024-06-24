using QuizzApp.Models;

namespace QuizzApp.Interfaces.Solutions
{
    public interface ISolutionService
    {
        public Task<(List<Solution>,List<Option>)> GetSolutions(List<Question> question);
    }
}
