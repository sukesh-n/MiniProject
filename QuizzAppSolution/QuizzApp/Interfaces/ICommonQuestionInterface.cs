using QuizzApp.Models.DTO;

namespace QuizzApp.Interfaces
{
    public interface ICommonQuestionInterface
    {
        public Task<QuestionWithCategoryDTO> GetQuestionWithCategory(QuestionWithCategoryDTO questionWithCategoryDTO);
    }
}
