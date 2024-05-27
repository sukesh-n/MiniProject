using QuizzApp.Models.DTO;

namespace QuizzApp.Interfaces
{
    public interface ICandidateInterface
    {
        public Task<QuestionDTO> AttendTest(QuestionDTO questionDTO);
    }
}
