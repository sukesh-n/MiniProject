using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizzApp.Services;
using QuizzApp.Models.DTO;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Interfaces.ResultInterface;
using QuizzApp.Models.DTO.AnalyseServiceDTO;
using QuizzApp.Models;

namespace QuizzApp.Tests.Services
{
    [TestFixture]
    public class TestServiceUnitTest
    {
        private TestService _testService;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ITestRepository> _mockTestRepository;
        private Mock<IAssignedQuestionRepository> _mockAssignedQuestionRepository;
        private Mock<IQuestionService> _mockQuestionService;
        private Mock<IResultRepository> _mockResultRepository;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockTestRepository = new Mock<ITestRepository>();
            _mockAssignedQuestionRepository = new Mock<IAssignedQuestionRepository>();
            _mockQuestionService = new Mock<IQuestionService>();
            _mockResultRepository = new Mock<IResultRepository>();

            _testService = new TestService(
                _mockTestRepository.Object,
                Mock.Of<IQuestionRepository>(),
                _mockAssignedQuestionRepository.Object,
                _mockQuestionService.Object,
                _mockUserRepository.Object,
                _mockResultRepository.Object
            );
        }


        [Test]
        public void AttendTest_UserNotFound_ThrowsException()
        {
            // Arrange
            var questionDTO = new List<QuestionDTO>();
            var testAssign = new TestAssign {  };
            var email = "nonexistent@example.com";

            _mockUserRepository.Setup(r => r.GetAllDetailsByUserEmailAsync(email))
                               .ReturnsAsync((List<User>)null);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _testService.AttendTest(questionDTO, testAssign.AssignmentNo, email));
        }


        [Test]
        public void AttendTest_QuestionsNotFound_ThrowsException()
        {
            // Arrange
            var questionDTO = new List<QuestionDTO>();
            var testAssign = new TestAssign {  };
            var email = "test@example.com";

            _mockUserRepository.Setup(r => r.GetAllDetailsByUserEmailAsync(email))
                               .ReturnsAsync(new List<User> { new User { UserId = 1, Role = "Candidate" } });

            _mockTestRepository.Setup(r => r.GetTestByUserIdAndAssignemtnNumber(1, testAssign.AssignmentNo))
                               .ReturnsAsync(new TestDTO { TestId = 1 });

            _mockAssignedQuestionRepository.Setup(r => r.GetQuestionByAssignmentNumber(testAssign.AssignmentNo))
                                          .ReturnsAsync((List<int>)null);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _testService.AttendTest(questionDTO, testAssign.AssignmentNo, email));
        }
    }
}
