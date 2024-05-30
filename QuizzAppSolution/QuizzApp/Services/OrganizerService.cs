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
        private readonly IRepository<int, Test> _testRepository;
        private readonly IRepository<int, Result> _resultrepository;
        private readonly IRepository<int, Models.Option> _optionRepository;
        private readonly IQuestionService _questionService;
        private readonly ISolutionService _solutionService;
        private readonly IAssignedTestRepository _assignedTestRepository;
        private readonly IAssignedTestEmailRepository _assignedTestEmailRepository;
        private readonly IAssignedQuestionRepository _assignedQuestionRepository;
        private readonly IUserRepository _userRepository;
        public OrganizerService(IQuestionRepository questionRepository, ICategoryRepository categoryRepository, ISolutionRepository solutionRepository, IRepository<int, Test> testRepository, IRepository<int, Result> resultrepository, IRepository<int, Option> optionRepository, IQuestionService questionService, ISolutionService solutionService, IUserRepository userRepository)
        {
            _questionRepository = questionRepository;
            _categoryRepository = categoryRepository;
            _solutionRepository = solutionRepository;
            _testRepository = testRepository;
            _resultrepository = resultrepository;
            _optionRepository = optionRepository;
            _questionService = questionService;
            _solutionService = solutionService;
            _userRepository = userRepository;
        }

        public OrganizerService(IAssignedTestRepository assignedTestRepository, IAssignedTestEmailRepository assignedTestEmailRepository, IAssignedQuestionRepository assignedQuestionRepository)
        {
            _assignedTestRepository = assignedTestRepository;
            _assignedTestEmailRepository = assignedTestEmailRepository;
            _assignedQuestionRepository = assignedQuestionRepository;
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
        public async Task<List<TestAssignDTO>> AssignTest(TestAssignDTO testAssignDTO, QuestionSelectionDTO questionSelectionDTO)
        {
            try
            {
                var GetQuestionsWithSolution = await GenerateQuizzApiWithSolution(questionSelectionDTO);
                var TestAssigner = await _assignedTestRepository.AddAsync(testAssignDTO);

                var EmailDetails = await _userRepository.GetAllDetailsByEmailsAsync(testAssignDTO.CandidateEmails);
                List<AssignedTestEmailDTO> assignedTestEmails = new List<AssignedTestEmailDTO>();
                foreach (var email in testAssignDTO.CandidateEmails)
                {
                    AssignedTestEmailDTO assignedTestEmailDTO = new AssignedTestEmailDTO();
                    var userDetails = EmailDetails.FirstOrDefault(u => u.UserEmail == email);
                    if (userDetails != null)
                    {
                        assignedTestEmailDTO.AssignmentNumber = TestAssigner.AssignmentNo;
                        assignedTestEmailDTO.Email = email;                        
                        if(userDetails.Role.ToLower()=="organizer")
                            assignedTestEmailDTO.IsOrganizer = true;
                        if (userDetails.Role.ToLower() == "candidate")
                            assignedTestEmailDTO.IsCandidate = true;
                        if (userDetails.Role.ToLower() == "admin")
                            assignedTestEmailDTO.IsAdmin = true;
                    }
                    else
                    {
                        assignedTestEmailDTO.AssignmentNumber=TestAssigner.AssignmentNo;
                        assignedTestEmailDTO.Email = email;
                        assignedTestEmailDTO.IsOrganizer = false;
                        assignedTestEmailDTO.IsCandidate = false;
                        assignedTestEmailDTO.IsAdmin= false;
                        
                    }
                    assignedTestEmails.Add(assignedTestEmailDTO);
                }
                var EmailUpload = await _assignedTestEmailRepository.AddEmailsForTest(TestAssigner.AssignmentNo,assignedTestEmails);
                if (EmailUpload == false)
                {
                    throw new UnableToAddException();
                }
                return TestAssigner;
            }
        }

    }
}
