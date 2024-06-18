using Microsoft.EntityFrameworkCore;
using Moq;
using QuizzApp.Context;
using QuizzApp.Models;
using QuizzApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzAppTest.Services.Test
{
    internal class TestRepositoryTest
    {
        private TestRepository _testRepository;
        private QuizzAppContext _mockContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<QuizzAppContext>()
                .UseInMemoryDatabase(databaseName: "QuizzAppTestDb")
                .Options;

            _mockContext = new QuizzAppContext(options);

            _mockContext.Database.EnsureCreated();

            _testRepository = new TestRepository(_mockContext);
        }

        [Test]
        public async Task AddTestListAsync_Should_Add_Test_List()
        {
            // Arrange
            var testDTOs = new List<TestDTO>
            {
                new TestDTO { AssignmentNo = 1, TestId = 1, UserId = 1, QuestionsCount = 10, StatusOfTest = "In Progress", TestStartDate = DateTime.Now, TestEndDate = DateTime.Now.AddDays(1), TestType = "Mock Test" },
                new TestDTO { AssignmentNo = 2, TestId = 2, UserId = 2, QuestionsCount = 5, StatusOfTest = "Completed", TestStartDate = DateTime.Now, TestEndDate = DateTime.Now.AddDays(1), TestType = "Practice Test" }
            };

            // Act
            var result = await _testRepository.AddTestListAsync(testDTOs);

            // Assert
            Assert.AreEqual(testDTOs, result);
        }

        [Test]
        public async Task GetTestByAssignmentNoAsync_Should_Return_TestDTO()
        {
            // Arrange
            var userId = 1;
            var assignmentNo = 1;
            var testEntity = new QuizzApp.Models.Test
            {
                AssignmentNo = assignmentNo,
                TestId = 11,
                UserId = userId,
                QuestionsCount = 10,
                StatusOfTest = "In Progress",
                TestStartDate = DateTime.Now,
                TestEndDate = DateTime.Now.AddDays(1),
                TestType = "Mock Test"
            };

            await _mockContext.tests.AddAsync(testEntity);
            await _mockContext.SaveChangesAsync();

            // Act
            var result = await _testRepository.GetTestByAssignmentNoAsync(assignmentNo);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.AssignmentNo, Is.EqualTo(testEntity.AssignmentNo));
        }

        [Test]
        public async Task GetTestByUserIdAndAssignemtnNumber_Should_Return_TestDTO()
        {
            // Arrange
            var userId = 1;
            var assignmentNumber = 2;
            var test = new QuizzApp.Models.Test
            {
                AssignmentNo = assignmentNumber,
                TestId = 10,
                UserId = userId,
                QuestionsCount = 10,
                StatusOfTest = "In Progress",
                TestStartDate = DateTime.Now,
                TestEndDate = DateTime.Now.AddDays(1),
                TestType = "Mock Test"
            };

            await _mockContext.tests.AddAsync(test);
            await _mockContext.SaveChangesAsync();

            // Act
            var result = await _testRepository.GetTestByUserIdAndAssignemtnNumber(userId, assignmentNumber);

            var resultDTO = new TestDTO
            {
                AssignmentNo = result.AssignmentNo,
                TestId = result.TestId,
                UserId = result.UserId,
                QuestionsCount = result.QuestionsCount,
                StatusOfTest = result.StatusOfTest,
                TestStartDate = result.TestStartDate,
                TestEndDate = result.TestEndDate,
                TestType = result.TestType
            };

            // Assert
            Assert.IsNotNull(resultDTO);
            Assert.That(resultDTO.AssignmentNo, Is.EqualTo(test.AssignmentNo));
            Assert.That(resultDTO.TestType, Is.EqualTo(test.TestType));
        }
    }

}
