using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Repositories;
using static QuizzApp.Models.Option;

namespace QuizzApp.Services
{
    public class QuestionServices : IQuestionService
    {
        private readonly IRepository<int, Question> _questionRepository;
        private readonly IRepository<int, Solution> _solutionRepository;
        private readonly IRepository<int, Option> _optionRepository;
        private readonly IRepository<int, Category> _categoryRepository;

        public QuestionServices(IRepository<int, Question> questionRepository, IRepository<int, Solution> solutionRepository, IRepository<int, Option> optionRepository, IRepository<int, Category> categoryRepository)
        {
            _questionRepository = questionRepository;
            _solutionRepository = solutionRepository;
            _optionRepository = optionRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> AddQuestionWithAnswerAsync(QuestionSolutionDTO questionSolutionDTO)
        {

            var category = new Category()
            {
                MainCategory = questionSolutionDTO.MainCategory,
                SubCategory = questionSolutionDTO.SubCategory
            };
            var CategoryResult = await _categoryRepository.AddAsync(category);

            var question = new Question()
            {
                CategoryId = CategoryResult.CategoryId,
                QuestionDescription = questionSolutionDTO.QuestionDescription,
                QuestionType = questionSolutionDTO.QuestionType,
                DifficultyLevel = questionSolutionDTO.QuestionDifficultyLevel
            };
            var QuestionResult = await _questionRepository.AddAsync(question);
            Solution solution = null;

            if (questionSolutionDTO.NumericalAnswer != null || questionSolutionDTO.TrueFalseAnswer != null)
            {
                solution = new Solution()
                {
                    QuestionId = QuestionResult.QuestionId,
                    NumericalAnswer = questionSolutionDTO.NumericalAnswer,
                    TrueFalseAnswer = questionSolutionDTO.TrueFalseAnswer
                };
                var SolutionResult = await _solutionRepository.AddAsync(solution);
            }
            else if (questionSolutionDTO.Options != null && questionSolutionDTO.Options != null)
            {
                int OptionID = 1;
                foreach (var option in questionSolutionDTO.Options)
                {
                    var optionSolution = new Option()
                    {
                        QuestionId = QuestionResult.QuestionId,
                        OptionDescription = OptionDescriptionType.String,
                        Optionid = OptionID,
                        Value = option
                    };
                    var OptionResult = await _optionRepository.AddAsync(optionSolution);
                    OptionID++;
                }
                solution = new Solution()
                {
                    QuestionId = QuestionResult.QuestionId,
                    CorrectOptionAnswer = questionSolutionDTO.CorrectOptionAnswer
                };
            }

            if (solution == null)
                return false;

            await _solutionRepository.AddAsync(solution);

            return true;
        }
        public Task<QuestionWithCategoryDTO> GetQuestionWithCategory(QuestionWithCategoryDTO questionWithCategoryDTO)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionSolutionDTO> UpdateQuestionsWithSolution(QuestionSolutionDTO questionSolutionDTO)
        {
            throw new NotImplementedException();
        }
    }
}
