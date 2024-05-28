using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.AnalyseServiceDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzApp.Services
{
    public class AdminService : IAdminInterface
    {
        private readonly IRepository<int,Question> _questionRepository;
        private readonly IRepository<int,Category> _categoryRepository;
        private readonly IRepository<int,Solution> _solutionRepository;
        private readonly IRepository<int,Test> _testRepository;
        private readonly IRepository<int,Result> _resultrepository;
        private readonly IRepository<int,Models.Option> _optionRepository;
        private readonly IRepository<int,Security> _securityRepository;
        private readonly IUserRepository _userRepository;

        public AdminService(IRepository<int, Question> questionRepository, IRepository<int, Category> categoryRepository, IRepository<int, Solution> solutionRepository, IRepository<int, Test> testRepository, IRepository<int, Result> resultrepository, IRepository<int, Models.Option> optionRepository, IRepository<int, Security> securityRepository, IUserRepository userRepository)
        {
            _questionRepository = questionRepository;
            _categoryRepository = categoryRepository;
            _solutionRepository = solutionRepository;
            _testRepository = testRepository;
            _resultrepository = resultrepository;
            _optionRepository = optionRepository;
            _securityRepository = securityRepository;
            _userRepository = userRepository;
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
                QuestionType=questionSolutionDTO.QuestionType,
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
            else if (questionSolutionDTO.Option != null)
            {
                foreach (var option in questionSolutionDTO.Option)
                {
                    var optionEntity = new OptionDTO()
                    {
                        QuestionId = QuestionResult.QuestionId,
                        OptionDescription = option
                        
                    };

                    var OptionResult = await _optionRepository.AddAsync(optionEntity);
                }
                solution = new Solution()
                {
                    QuestionId = QuestionResult.QuestionId,
                   
                };
                var SolutionResult = await _solutionRepository.AddAsync(solution);
            }

            if (solution == null)
                return false;

            await _solutionRepository.AddAsync(solution);

            return true;
        }
        public async Task<TestDTO> ConductCommonQuizAsync(List<User> candidates, Test quiz)
        {
            await Task.Delay(1000); 

            var testDTO = new TestDTO
            {
                
            };

            return testDTO;
        }

        public Task<QuestionWithCategoryDTO> GetQuestionWithCategory(QuestionWithCategoryDTO questionWithCategoryDTO)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionSolutionDTO> UpdateQuestionsWithSolution(QuestionSolutionDTO questionSolutionDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> ViewResultAnalysis()
        {
            throw new NotImplementedException();
        }
    }
}
