using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces
{
    public interface IQuestionRepository : IRepository<int,Question>
    {
        public Task<List<QuestionTypeCount>> GetTypesAndTheirCount(int DifficultyLevel);
        public Task<List<QuestionTypeCountBasedOnCategory>> GetTypesAndTheirCountByCategory(string MainCategory, string SubCategory);
        public Task<List<Question>> GetFilteredQuestions(QuestionSelectionDTO questionSelectionDTO,List<int> CategoryIds);
    }



    public class QuestionCount
    {
        public int Count { get; set; }
    }
    public class QuestionTypeCount : QuestionCount
    {
        public string Type { get; set; }
    }
    public class QuestionTypeCountBasedOnCategory : QuestionTypeCount
    {
        public int CategoryId { get; set; }
    }
}
