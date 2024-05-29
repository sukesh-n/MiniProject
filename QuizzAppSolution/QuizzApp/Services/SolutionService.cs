using QuizzApp.Interfaces.Solutions;
using QuizzApp.Models;
using QuizzApp.Exceptions;
namespace QuizzApp.Services
{
    public class SolutionService : ISolutionService
    {
        private readonly ISolutionRepository _solutionRepository;

        public SolutionService(ISolutionRepository solutionRepository)
        {
            _solutionRepository = solutionRepository;
        }

        public async Task<List<Solution>> GetSolutions(List<Question> question)
        {
            try
            {
                List<int> QuestionIDs = new List<int>();
                foreach (Question questionID in question)
                {
                    QuestionIDs.Add(questionID.QuestionId);
                }
                var getSolutions = await _solutionRepository.GetSolutions(QuestionIDs);
                if (getSolutions == null)
                {
                    throw new EmptyRepositoryException("empty solution");
                }
                return getSolutions;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository();
            }
        }
    }
}
