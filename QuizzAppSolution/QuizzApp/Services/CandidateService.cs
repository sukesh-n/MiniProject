using QuizzApp.Interfaces;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.AnalyseServiceDTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Services
{
    public class CandidateService : ICandidateService
    {
        public Task<QuestionDTO> AttendAssignedTest(QuestionDTO questionDTO)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionDTO> GetMyQuestion(int testId, object userId)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionDTO> TakeCustomTest(QuestionSelectionDTO questionSelectionDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> ViewRank(ResultDTO resultDTO)
        {
            throw new NotImplementedException();
        }
    }
}
