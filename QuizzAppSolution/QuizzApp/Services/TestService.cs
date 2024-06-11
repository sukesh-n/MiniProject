using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Models.DTO.AnalyseServiceDTO;
using QuizzApp.Interfaces.ResultInterface;

namespace QuizzApp.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IAssignedQuestionRepository _assignedQuestionRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionService _questionService;
        private readonly IUserRepository _userRepository;
        private readonly IResultRepository _resultRepository;

        public TestService(ITestRepository testRepository, IQuestionRepository questionRepository, IAssignedQuestionRepository assignedQuestionRepository, IQuestionService questionService, IUserRepository userRepository, IResultRepository resultRepository)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _assignedQuestionRepository = assignedQuestionRepository;
            _questionService = questionService;
            _userRepository = userRepository;
            _resultRepository = resultRepository;
        }

        public Task<NotificationDTO> AssignTest(TestAssign testDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionDTO>> AttendTest(List<QuestionDTO> questionDTO, int AssignmentNumber, string email)
        {
            try
            {
                var CheckUserIsCandidate = await _userRepository.GetAllDetailsByUserEmailAsync(email);
                if (CheckUserIsCandidate == null)
                {
                    throw new Exception("User Not Found");
                }
                int UserId = 0;
                foreach (var user in CheckUserIsCandidate)
                {
                    if (user.Role.ToLower() != "candidate")
                    {
                        throw new Exception("User Not Candidate");
                    }
                    if (user.Role.ToLower() == "candidate")
                    {
                        UserId= user.UserId;
                    }
                }


                var GetTestId = await _testRepository.GetTestByUserIdAndAssignemtnNumber(UserId, AssignmentNumber);
                if (GetTestId == null)
                {
                    throw new Exception("Test Not Found");
                }
                var GetAssignedQuestions = await _assignedQuestionRepository.GetQuestionByAssignmentNumber(AssignmentNumber);
                if (GetAssignedQuestions == null)
                {
                    throw new Exception("No Questions Found");
                }

                List<int> QuestionIds = new List<int>();
                foreach (var question in questionDTO)
                {
                    if (GetAssignedQuestions.Contains(question.QuestionId))
                    {
                        QuestionIds.Add(question.QuestionId);
                        continue;
                    }
                    else
                    {
                        throw new Exception("Question Not Found");
                    }
                }

                var GeSolution = await _questionService.GetSolutionForQUestions(QuestionIds);
                if (GeSolution == null)
                {
                    throw new Exception("No Solution Found");
                }
                List<QuestionDTO> QuestionWithSolutions = new List<QuestionDTO>();
                int score = 0;
                foreach (var question in questionDTO)
                {
                    foreach (var solution in GeSolution)
                    {
                        QuestionDTO QuestionWithSolution = new QuestionDTO();
                        if (question.QuestionId == solution.QuestionId)
                        {
                            QuestionWithSolution.QuestionId = question.QuestionId;
                            QuestionWithSolution.QuestionType = question.QuestionType;
                            QuestionWithSolution.QuestionDescription = question.QuestionDescription;
                            QuestionWithSolution.DifficultyLevel = question.DifficultyLevel;
                            QuestionWithSolution.CategoryId = question.CategoryId;
                            QuestionWithSolution.TrueFalseAnswer = null;
                            QuestionWithSolution.NumericalAnswer = null;
                            QuestionWithSolution.CorrectOptionAnswer=null;
                            if (question.QuestionType == "MCQ" && question.CorrectOptionAnswer==solution.CorrectOptionAnswer)
                            {
                                QuestionWithSolution.CorrectOptionAnswer = solution.CorrectOptionAnswer;
                                score++;
                            }
                            else if(question.QuestionType == "True/False" && question.TrueFalseAnswer==solution.TrueFalseAnswer)
                            {
                                QuestionWithSolution.TrueFalseAnswer = solution.TrueFalseAnswer;
                                score++;
                            }
                            else if(question.QuestionType == "Numerical" && question.NumericalAnswer==solution.NumericalAnswer)
                            {
                                QuestionWithSolution.NumericalAnswer = solution.NumericalAnswer;
                                score++;
                            }

                            QuestionWithSolutions.Add(QuestionWithSolution);
                        }
                    }
                }

                var scoreUpload = new ResultDTO()
                {
                    TestId =  GetTestId.TestId,
                    score = score
                };

                var UploadScore = await _resultRepository.AddAsync(scoreUpload);
                if (UploadScore == null)
                {
                    throw new Exception("Score Not Uploaded");
                }
                return QuestionWithSolutions;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<TestAssign> ChooseQuestion(QuestionSelectionDTO questionSelectionDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionDTO>> GetTestQuestions(int AssignmentNumber, string email)
        {
            var getQuestionIds = await _assignedQuestionRepository.GetQuestionByAssignmentNumber(AssignmentNumber);

            if (getQuestionIds == null)
            {
                throw new Exception("No Questions Found");
            }

            var QuestionsList = await _questionService.GetQuestionById(getQuestionIds);
            if (QuestionsList == null)
            {
                throw new Exception("No Questions Found");
            }
            return QuestionsList;
            
        }

        public async Task<List<TestDTO>> PublishTest(List<TestDTO> testDTO)
        {
            try
            {
                var TestPublish = await _testRepository.AddTestListAsync(testDTO);
                return TestPublish;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
