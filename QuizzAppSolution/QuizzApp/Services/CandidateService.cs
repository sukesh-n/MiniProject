using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.AnalyseServiceDTO;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Repositories;

namespace QuizzApp.Services
{
    

    public class CandidateService : ICandidateService
    {
        private readonly IQuestionService _questionService;
        private readonly IAssignedTestRepository _assignedTestRepository;
        private readonly IAssignedQuestionRepository _assignedQuestionRepository;
        private readonly ITestService _testService;

        public CandidateService(IQuestionService questionService, IAssignedTestRepository assignedTestRepository, IAssignedQuestionRepository assignedQuestionRepository, ITestService testService)
        {
            _questionService = questionService;
            _assignedTestRepository = assignedTestRepository;
            _assignedQuestionRepository = assignedQuestionRepository;
            _testService = testService;
        }

        public Task<QuestionDTO> AttendAssignedTest(QuestionDTO questionDTO)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionDTO> GetMyQuestion(int testId, object userId)
        {
            throw new NotImplementedException();
        }

        public async Task<(List<QuestionDTO>,int TestId,int AssignmentNumber)> GetRandomQuizz(QuestionSelectionDTO questionSelectionDTO,int UserId)
        {
            try
            {
                var question = await _questionService.GetQuestionWithCategory(questionSelectionDTO);
                if (question == null)
                {
                    throw new Exception("No Question Found");
                }
                List<QuestionDTO> questionDTO = new List<QuestionDTO>();

                foreach (var item in question)
                {
                    questionDTO.Add(new QuestionDTO
                    {
                        QuestionId = item.QuestionId,
                        QuestionDescription = item.QuestionDescription,
                        QuestionType = item.QuestionType,
                        CategoryId = item.CategoryId,
                        DifficultyLevel = item.DifficultyLevel,
                        CorrectOptionAnswer = null,
                        NumericalAnswer = null,
                        TrueFalseAnswer = null
                    });
                }

                TestAssign testAssign = new TestAssign();
                {
                    testAssign.AssignedBy= UserId;
                    testAssign.TestDuration = question.Count * 2;
                    testAssign.TestName = "Self Quizz";
                    testAssign.StartTimeWindow = DateTime.Now;
                    testAssign.EndTimeWindow = DateTime.Now.AddMinutes(question.Count * 2);

                }

               
                var TestAssigner = await _assignedTestRepository.AddAsync(testAssign);

                List<QuestionSolutionDTO> questionSolutionDTO = new List<QuestionSolutionDTO>();
                foreach (var item in questionDTO)
                {
                    questionSolutionDTO.Add(new QuestionSolutionDTO
                    {
                        QuestionId = item.QuestionId,
                        QuestionDescription = item.QuestionDescription,
                        QuestionType = item.QuestionType,
                        QuestionDifficultyLevel = item.DifficultyLevel
                    });

                }

               
                var AddQuestion = await _assignedQuestionRepository.AddQuestionsForTest(TestAssigner.AssignmentNo, questionSolutionDTO);


                var TestId = 0;
                if (AddQuestion != null)
                {
                    List<TestDTO> testDTOs = new List<TestDTO>();
                    {
                        TestDTO testDTO = new TestDTO();
                        testDTO.UserId = UserId;
                        testDTO.AssignmentNo = TestAssigner.AssignmentNo;
                        testDTO.TestType = "Self test";
                        testDTO.QuestionsCount = question.Count;


                        testDTOs.Add(testDTO);
                    }
                    var PublishTestToCandidate = await _testService.PublishTest(testDTOs);
                    if (PublishTestToCandidate != null)
                    {
                        var result = PublishTestToCandidate.FirstOrDefault(m => m.AssignmentNo == TestAssigner.AssignmentNo && m.UserId == UserId);
                        TestId = result.TestId;
                    }
                    else
                    {
                        throw new UnableToAddException();
                    }
                }
                else
                {
                    throw new UnableToAddException();
                }
                return (questionDTO,TestId,TestAssigner.AssignmentNo);
            }
            catch
            {
                throw new Exception("Unable to get question");
            }
        }

        public async Task<(List<QuestionDTO>,ScoreDTO)> TakeCustomTest(List<QuestionDTO> questionDTO, int TestId,int AssignmentNumber,string email)
        {
            try
            {
                
                var GetAssignnedQuestion = await _testService.AttendTest(questionDTO, AssignmentNumber,email);
                if (GetAssignnedQuestion.Item1 == null && GetAssignnedQuestion.Item2==null)
                {
                    throw new Exception("Unable to get question");
                }
                return (GetAssignnedQuestion.Item1,GetAssignnedQuestion.Item2);

            }
            catch
            {
                throw new Exception("failed");
            }
        }

        public Task<ResultDTO> ViewRank(ResultDTO resultDTO)
        {
            throw new NotImplementedException();
        }
    }
}
