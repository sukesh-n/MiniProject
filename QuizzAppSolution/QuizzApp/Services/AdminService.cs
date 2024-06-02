using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Solutions;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.AnalyseServiceDTO;
using QuizzApp.Models.DTO.Test;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static QuizzApp.Models.Option;

namespace QuizzApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISolutionRepository _solutionRepository;
        private readonly ITestRepository _testRepository;
        private readonly IRepository<int,Result> _resultrepository;
        private readonly IRepository<int,Models.Option> _optionRepository;
        private readonly IRepository<int,Security> _securityRepository;
        private readonly IUserRepository _userRepository;

        public AdminService(IQuestionRepository questionRepository, ICategoryRepository categoryRepository, ISolutionRepository solutionRepository, ITestRepository testRepository, IRepository<int, Result> resultrepository, IRepository<int, Models.Option> optionRepository, IRepository<int, Security> securityRepository, IUserRepository userRepository)
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

        
        public async Task<TestAssign> ConductCommonQuizAsync(List<User> candidates, QuestionSelectionDTO questionSelectionDTO)
        {
            TestAssign testDTO = new TestAssign()
            {

            };

            return testDTO;
        }

        public Task<QuestionWithCategoryDTO> GetQuestionWithCategory(QuestionWithCategoryDTO questionWithCategoryDTO)
        {
            throw new NotImplementedException();
        }


        public Task<ResultDTO> ViewResultAnalysis()
        {
            throw new NotImplementedException();
        }


    }
}
