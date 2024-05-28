using QuizzApp.Models.DTO;

namespace QuizzApp.Interfaces
{
    public interface ICandidateService
    {
        public Task<QuestionDTO> AttendTest(QuestionDTO questionDTO);
    }
}
