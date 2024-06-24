using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.AnalyseServiceDTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces
{
    public interface ICandidateService
    {
        public Task<QuestionDTO> AttendAssignedTest(QuestionDTO questionDTO);
        public Task<QuestionDTO> GetMyQuestion(int testId, object userId);
        public Task<(List<QuestionDTO>,ScoreDTO)> TakeCustomTest(List<QuestionDTO> questionDTO,int TestId, int AssignmentNumber, string email);
        public Task<ResultDTO> ViewRank(ResultDTO resultDTO);
        public Task<(List<QuestionDTO>,int TestId,int AssignmentNumber)> GetRandomQuizz(QuestionSelectionDTO questionSelectionDTO,int UserId);
    }
}
