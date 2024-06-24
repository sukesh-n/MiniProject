using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Services;
using QuizzApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Models.DTO;
using QuizzApp.Repositories;
using QuizzApp.Context;
using Moq;
using QuizzApp.Interfaces.ResultInterface;

namespace QuizzApp.Tests.Services
{
    [TestFixture]
    public class CandidateServiceTests
    {
        private CandidateService _candidateService;
        private DbContextOptions<QuizzAppContext> _dbContextOptions;

        [SetUp]
        public void SetUp()
        {
            // Initialize DbContext options for in-memory database
            _dbContextOptions = new DbContextOptionsBuilder<QuizzAppContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            // Seed the database with test data
            using (var context = new QuizzAppContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Add seed data for categories, questions, etc.
                var categories = new List<Category>
                {
                    new Category { CategoryId = 1, MainCategory = "TestCategory", SubCategory = "SubCategory" }
                };
                context.categories.AddRange(categories);

                var questions = new List<Question>
                {
                    new Question { QuestionId = 1, QuestionDescription = "Question 1", DifficultyLevel = 1, CategoryId = 1, QuestionType = "MCQ" }
                };
                context.questions.AddRange(questions);

                // Add seed data for users
                var users = new List<User>
                {
                    new User { UserId = 1, UserEmail = "test@example.com", UserName = "Test", Role="candidate" }
                };
                context.users.AddRange(users);

                context.SaveChanges();
            }

            // Create repositories and services
            var dbContext = new QuizzAppContext(_dbContextOptions);
            var questionRepository = new QuestionRepository(dbContext);
            var solutionRepository = new SolutionRepository(dbContext);
            var optionsRepository = new OptionsRepository(dbContext);
            var categoryRepository = new CategoryRepository(dbContext);
            var questionService = new QuestionServices(questionRepository, solutionRepository, optionsRepository, categoryRepository);

            var assignedTestRepository = new AssigningTestRepository(dbContext);
            var assignedQuestionRepository = new AssignedQuestionsRepository(dbContext);

            // Mock repositories or actual implementation
            var mockTestRepository = new Mock<ITestRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockResultRepository = new Mock<IResultRepository>();

            // Configure mock repositories for specific test scenarios
            // ...

            var testService = new TestService(
                mockTestRepository.Object,
                questionRepository,
                assignedQuestionRepository,
                questionService,
                mockUserRepository.Object,
                mockResultRepository.Object
            );

            _candidateService = new CandidateService(questionService, assignedTestRepository, assignedQuestionRepository, testService);
        }

        [Test]
        public async Task GetRandomQuizz_ValidInput_Success()
        {
            // Arrange
            var questionSelectionDTO = new QuestionSelectionDTO
            {
                MainCategory = "TestCategory",
                SubCategory = "SubCategory",
                DifficultyLevel = 1,
                Type = "MCQ"
            };
            int userId = 1;

            // Act
            var result = await _candidateService.GetRandomQuizz(questionSelectionDTO, userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Count > 0);
            Assert.GreaterOrEqual(result.TestId, 0);
            Assert.GreaterOrEqual(result.AssignmentNumber, 0); 
        }



        [Test]
        public async Task TakeCustomTest_ValidInput_Success()
        {
            // Arrange
            var questionDTOs = new List<QuestionDTO>
            {
                new QuestionDTO { QuestionId = 1, QuestionDescription = "Question 1", DifficultyLevel = 1 }
            };
            int testId = 1;
            int assignmentNumber = 1;
            string email = "test@example.com";

            // Mock the TestService.AttendTest method to return a successful result
            var mockTestRepository = new Mock<ITestRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockResultRepository = new Mock<IResultRepository>();
            var mockTestService = new Mock<ITestService>();
            mockTestService.Setup(ts => ts.AttendTest(questionDTOs, assignmentNumber, email))
                .ReturnsAsync((questionDTOs, new ScoreDTO { Score = 50 })); // Mock a score

            _candidateService = new CandidateService(
                Mock.Of<IQuestionService>(),
                Mock.Of<IAssignedTestRepository>(),
                Mock.Of<IAssignedQuestionRepository>(),
                mockTestService.Object
            );

            // Act
            var result = await _candidateService.TakeCustomTest(questionDTOs, testId, assignmentNumber, email);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Count > 0); // Ensure questions were retrieved
            Assert.IsNotNull(result.Item2); // Ensure ScoreDTO is not null
            Assert.AreEqual(50, result.Item2.Score); // Verify the score
        }

        [Test]
        public async Task TakeCustomTest_Failure_ExceptionThrown()
        {
            // Arrange
            var questionDTOs = new List<QuestionDTO>
    {
        new QuestionDTO { QuestionId = 1, QuestionDescription = "Question 1", DifficultyLevel = 1 }
    };
            int testId = 1;
            int assignmentNumber = 1;
            string email = "test@example.com";

            // Mock the TestService.AttendTest method to throw an exception with the specific message
            var mockTestService = new Mock<ITestService>();
            mockTestService.Setup(ts => ts.AttendTest(questionDTOs, assignmentNumber, email))
                .ThrowsAsync(new Exception("Unable to get question"));

            _candidateService = new CandidateService(
                Mock.Of<IQuestionService>(),
                Mock.Of<IAssignedTestRepository>(),
                Mock.Of<IAssignedQuestionRepository>(),
                mockTestService.Object
            );

            // Act + Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _candidateService.TakeCustomTest(questionDTOs, testId, assignmentNumber, email));
            Assert.That(ex.Message, Is.EqualTo("failed"));
        }
    }

    // Mock DbContext for testing purposes
    public class AppDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<TestAssign> testAssigns { get; set; } // Add DbSet for TestAssign
        public DbSet<Test> tests { get; set; } // Add DbSet for Test
        public DbSet<AssignedQuestions> assignedQuestions { get; set; } // Add DbSet for AssignedQuestion

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}