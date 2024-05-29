using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.AnalyseServiceDTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces
{
    public interface ICandidateService
    {
        public Task<QuestionDTO> AttendAssignedTest(QuestionDTO questionDTO);
        public Task<QuestionDTO> GetMyQuestion(int testId, object userId);
        public Task<QuestionDTO> TakeCustomTest(QuestionSelectionDTO questionSelectionDTO);
        public Task<ResultDTO> ViewRank(ResultDTO resultDTO);
    }
}
