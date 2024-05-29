using QuizzApp.Models;

namespace QuizzApp.Interfaces.Solutions
{
    public interface ISolutionService
    {
        public Task<List<Solution>> GetSolutions(List<Question> question);
    }
}
