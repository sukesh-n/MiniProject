using QuizzApp.Interfaces.Solutions;
using QuizzApp.Models;
using QuizzApp.Exceptions;
using QuizzApp.Repositories;
namespace QuizzApp.Services
{
    public class SolutionService : ISolutionService
    {
        private readonly ISolutionRepository _solutionRepository;
        private readonly IOptionsRepository _optionsRepository;

        public SolutionService(ISolutionRepository solutionRepository, IOptionsRepository optionsRepository)
        {
            _solutionRepository = solutionRepository;
            _optionsRepository = optionsRepository;
        }

        public async Task<(List<Solution>,List<Option>)> GetSolutions(List<Question> question)
        {
            try
            {
                List<int> QuestionIDs = new List<int>();
                List<int> MCQIds = new List<int>();
                foreach (Question questionID in question)
                {
                    if (questionID.QuestionType == "MCQ")
                    {
                        MCQIds.Add(questionID.QuestionId);
                    }
                    QuestionIDs.Add(questionID.QuestionId);
                }
                var getSolutions = await _solutionRepository.GetSolutions(QuestionIDs);
                if (getSolutions == null)
                {
                    throw new EmptyRepositoryException("empty solution");
                }
                var getOptions = await _optionsRepository.GetAllByQuestionIdAsync(MCQIds);
                if (getOptions == null)
                {
                    return (getSolutions,null);
                }
                return (getSolutions,getOptions);
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository();
            }
        }
    }
}
