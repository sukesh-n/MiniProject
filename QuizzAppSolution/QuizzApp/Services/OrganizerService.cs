using Microsoft.Extensions.Options;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Solutions;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Services
{
    public class OrganizerService : IOrganizerService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISolutionRepository _solutionRepository;
        private readonly ITestRepository _testRepository;
        private readonly IRepository<int, Result> _resultrepository;
        private readonly IRepository<int, Models.Option> _optionRepository;
        private readonly IQuestionService _questionService;
        private readonly ISolutionService _solutionService;
        private readonly ITestService _testService;
        private readonly IAssignedTestRepository _assignedTestRepository;
        private readonly IAssignedTestEmailRepository _assignedTestEmailRepository;
        private readonly IAssignedQuestionRepository _assignedQuestionRepository;
        private readonly IUserRepository _userRepository;
        public OrganizerService(
            IQuestionRepository questionRepository,
            ICategoryRepository categoryRepository,
            ISolutionRepository solutionRepository,
            ITestRepository testRepository,
            IRepository<int, Result> resultrepository,
            IRepository<int, Option> optionRepository,
            IQuestionService questionService,
            ISolutionService solutionService,
            IUserRepository userRepository,
            IAssignedTestRepository assignedTestRepository,
            IAssignedTestEmailRepository assignedTestEmailRepository,
            IAssignedQuestionRepository assignedQuestionRepository,
            ITestService testService)
        {
            _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _solutionRepository = solutionRepository ?? throw new ArgumentNullException(nameof(solutionRepository));
            _testRepository = testRepository ?? throw new ArgumentNullException(nameof(testRepository));
            _resultrepository = resultrepository ?? throw new ArgumentNullException(nameof(resultrepository));
            _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
            _questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
            _solutionService = solutionService ?? throw new ArgumentNullException(nameof(solutionService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _assignedTestRepository = assignedTestRepository ?? throw new ArgumentNullException(nameof(assignedTestRepository));
            _assignedTestEmailRepository = assignedTestEmailRepository ?? throw new ArgumentNullException(nameof(assignedTestEmailRepository));
            _assignedQuestionRepository = assignedQuestionRepository ?? throw new ArgumentNullException(nameof(assignedQuestionRepository));
            _testService = testService ?? throw new ArgumentNullException(nameof(testService));
        }

        public async Task<List<QuestionSolutionDTO>> GenerateQuizzApiWithSolution(QuestionSelectionDTO questionSelectionDTO)
        {
            try
            {
                var GetQuestions = await _questionService.GetQuestionWithCategory(questionSelectionDTO);
                if (GetQuestions == null)
                {
                    throw new EmptyRepositoryException();

                }
                var GetSolutions = await _solutionService.GetSolutions(GetQuestions);
                if(GetSolutions == null)
                {

                throw new EmptyRepositoryException(); 
                }
                var quizQuestions = new List<QuestionSolutionDTO>();
                foreach (var question in GetQuestions)
                {
                    var questionDTO = new QuestionSolutionDTO
                    {
                        QuestionId = question.QuestionId,
                        QuestionDescription = question.QuestionDescription,
                        QuestionType = question.QuestionType
                    };
                    var solution = GetSolutions.Find(s => s.QuestionId == question.QuestionId);
                    if (solution != null)
                    {
                        questionDTO.NumericalAnswer = solution.NumericalAnswer;
                        questionDTO.TrueFalseAnswer = solution.TrueFalseAnswer;
                        questionDTO.QuestionDifficultyLevel = question.DifficultyLevel;
                       // questionDTO.Options = question.Options
                        questionDTO.CorrectOptionAnswer = solution.CorrectOptionAnswer;
                    }

                    quizQuestions.Add(questionDTO);
                }
                return quizQuestions;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<TestAssign> AssignTest(TestAssign testAssignDTO, QuestionSelectionDTO questionSelectionDTO)
        {
            try
            {
                // 1. Move to Question Service
                var GetQuestionsWithSolution = await GenerateQuizzApiWithSolution(questionSelectionDTO);
                var TestAssigner = await _assignedTestRepository.AddAsync(testAssignDTO);
                // 2. Move to User Service

                var EmailDetails = await _userRepository.GetAllDetailsByEmailsAsync(testAssignDTO.CandidateEmails);
                Dictionary<string, List<string>> userDetailsDict = EmailDetails
                    .GroupBy(u => u.UserEmail)
                    .ToDictionary(g => g.Key, g => g.Select(u => u.Role.ToLower()).ToList());

                List<AssignedTestEmailDTO> assignedTestEmails = new List<AssignedTestEmailDTO>();
                foreach (var email in testAssignDTO.CandidateEmails)
                {
                    AssignedTestEmailDTO assignedTestEmailDTO = new AssignedTestEmailDTO();
                    assignedTestEmailDTO.AssignmentNumber = TestAssigner.AssignmentNo;
                    assignedTestEmailDTO.Email = email;

                    if (userDetailsDict.TryGetValue(email, out List<string> roles))
                    {
                        assignedTestEmailDTO.IsOrganizer = roles.Contains("organizer");
                        assignedTestEmailDTO.IsCandidate = roles.Contains("candidate");
                        assignedTestEmailDTO.IsAdmin = roles.Contains("admin");
                    }
                    else
                    {
                        assignedTestEmailDTO.IsOrganizer = false;
                        assignedTestEmailDTO.IsCandidate = false;
                        assignedTestEmailDTO.IsAdmin = false;
                    }

                    assignedTestEmails.Add(assignedTestEmailDTO);
                }

                var EmailUpload = await _assignedTestEmailRepository.AddEmailsForTest(TestAssigner.AssignmentNo,assignedTestEmails);
                if (EmailUpload == false)
                {
                    throw new UnableToAddException();
                }                

                var AddQuestion = await _assignedQuestionRepository.AddQuestionsForTest(TestAssigner.AssignmentNo, GetQuestionsWithSolution);

                if (AddQuestion !=null )
                {
                    List<TestDTO> testDTOs = new List<TestDTO>();
                    foreach (var emailDetail in EmailDetails)
                    {
                        TestDTO testDTO = new TestDTO();
                        testDTO.UserId = emailDetail.UserId;
                        testDTO.AssignmentNo = TestAssigner.AssignmentNo;
                        testDTO.TestType = "Assignment";
                        testDTO.QuestionsCount = GetQuestionsWithSolution.Count;
                        

                        testDTOs.Add(testDTO);
                    }
                    var PublishTestToCandidate = await _testService.PublishTest(testDTOs);
                }
                else
                {
                    throw new UnableToAddException();
                }
                var FinalList = new TestAssign
                {
                    AssignmentNo = TestAssigner.AssignmentNo,
                    CandidateEmails = testAssignDTO.CandidateEmails
                };
                return FinalList;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
