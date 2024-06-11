using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Solutions;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Repositories;
using static QuizzApp.Models.Option;

namespace QuizzApp.Services
{
    public class QuestionServices : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ISolutionRepository _solutionRepository;
        private readonly IRepository<int, Option> _optionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public QuestionServices(IQuestionRepository questionRepository, ISolutionRepository solutionRepository, IRepository<int, Option> optionRepository, ICategoryRepository categoryRepository)
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

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories == null || !categories.Any())
                {
                    throw new EmptyDatabaseException("No categories");
                }
                return categories.ToList();
            }
            catch (EmptyDatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToFetchException("Unable to get categories", ex);
            }
        }

        public async Task<List<Question>> GetQuestionWithCategory(QuestionSelectionDTO questionSelectionDTO)
        {
            try
            {
                var CategoryIds = new List<int>();
                var questionSelectionDTONullRemoval = new QuestionSelectionDTO();

                if (questionSelectionDTO.NoOfQuestions != 0)
                    questionSelectionDTONullRemoval.NoOfQuestions = questionSelectionDTO.NoOfQuestions;
                else
                    questionSelectionDTONullRemoval.NoOfQuestions = 5;



                if (questionSelectionDTO.SubCategory != null)
                    if (questionSelectionDTO.MainCategory == null)
                        throw new InvalidFormatException("sub category cannot be found without main category");
                if (questionSelectionDTO.MainCategory != null)
                {
                    CategoryIds = await _categoryRepository.GetCategoryId(questionSelectionDTO.MainCategory, questionSelectionDTO.SubCategory);
                    if (CategoryIds == null)
                        throw new EmptyRepositoryException("No category found");
                }


                if (questionSelectionDTO.DifficultyLevel != 0)
                    questionSelectionDTONullRemoval.DifficultyLevel = questionSelectionDTO.DifficultyLevel;

                if (questionSelectionDTO.Type != null)
                    questionSelectionDTONullRemoval.Type = questionSelectionDTO.Type;

                
                var questionList = await _questionRepository.GetFilteredQuestions(questionSelectionDTONullRemoval, CategoryIds);
                if (questionList == null)
                {
                    throw new EmptyRepositoryException("No questions found");
                }
                return questionList;
            }
            catch
            {
                throw new UnableToFetchException("Unable to get Questions");
            }
        }

        public Task<QuestionSolutionDTO> UpdateQuestionsWithSolution(QuestionSolutionDTO questionSolutionDTO)
        {
            throw new NotImplementedException();
        }




        public async Task<List<QuestionDTO>> GetQuestionById(List<int> QuestionIds)
        {
            try
            {
                var QuestionList = await _questionRepository.GetQuestionById(QuestionIds);
                if (QuestionList == null)
                {
                    throw new EmptyRepositoryException("No questions found");
                }
                return QuestionList;
            }
            catch (EmptyRepositoryException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToFetchException("Unable to get questions", ex);
            }
        }

        public Task<List<Solution>> GetSolutionForQUestions(List<int> QuestionIds)
        {
            try
            {
                var Solution = _solutionRepository.GetSolutionForQuestions(QuestionIds);
                if (Solution == null)
                {
                    throw new EmptyRepositoryException("No solutions found");
                }
                return Solution;
            }
            catch (Exception ex)
            {
                throw new UnableToFetchException("Unable to get solutions", ex);
            }
        }
    }
}
