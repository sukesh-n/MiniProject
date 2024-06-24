using Moq;
using NUnit.Framework;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces.Solutions;
using QuizzApp.Models;
using QuizzApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzAppTest.Services
{
    [TestFixture]
    public class SolutionServiceTest
    {
        private Mock<ISolutionRepository> _mockSolutionRepository;
        private Mock<IOptionsRepository> _mockOptionsRepository;
        private SolutionService _solutionService;

        [SetUp]
        public void Setup()
        {
            _mockSolutionRepository = new Mock<ISolutionRepository>();
            _mockOptionsRepository = new Mock<IOptionsRepository>();

            _solutionService = new SolutionService(_mockSolutionRepository.Object, _mockOptionsRepository.Object);
        }

        [Test]
        public async Task GetSolutions_ValidQuestions_ReturnsSolutionsAndOptions()
        {
            // Arrange
            var questions = new List<Question>
            {
                new Question { QuestionId = 1, QuestionType = "MCQ" },
                new Question { QuestionId = 2, QuestionType = "MCQ" },
                new Question { QuestionId = 3, QuestionType = "Numerical" }
            };

            var solutions = new List<Solution>
            {
                new Solution { QuestionId = 1, CorrectOptionAnswer = "A" },
                new Solution { QuestionId = 2, CorrectOptionAnswer = "B" }
            };

            var options = new List<Option>
            {
                new Option { QuestionId = 1, Optionid = 1, OptionDescription = Option.OptionDescriptionType.String, Value = "Option A" },
                new Option { QuestionId = 1, Optionid = 2, OptionDescription = Option.OptionDescriptionType.String, Value = "Option B" },
                new Option { QuestionId = 2, Optionid = 3, OptionDescription = Option.OptionDescriptionType.String, Value = "Option C" },
                new Option { QuestionId = 2, Optionid = 4, OptionDescription = Option.OptionDescriptionType.String, Value = "Option D" }
            };

            _mockSolutionRepository.Setup(r => r.GetSolutions(It.IsAny<List<int>>()))
                                   .ReturnsAsync(solutions);

            _mockOptionsRepository.Setup(r => r.GetAllByQuestionIdAsync(It.IsAny<List<int>>()))
                                  .ReturnsAsync(options);

            // Act
            var (resultSolutions, resultOptions) = await _solutionService.GetSolutions(questions);

            // Assert
            Assert.IsNotNull(resultSolutions);
            Assert.IsNotNull(resultOptions);
            Assert.AreEqual(2, resultSolutions.Count);
            Assert.AreEqual(4, resultOptions.Count); 
        }

        [Test]
        public void GetSolutions_EmptySolutions_ThrowsEmptyRepositoryException()
        {
            // Arrange
            List<Question> questions = new List<Question>();
            _mockSolutionRepository.Setup(r => r.GetSolutions(It.IsAny<List<int>>()))
                                   .ReturnsAsync(() => null); 

            // Act and Assert
            Assert.ThrowsAsync<ErrorInConnectingRepository>(() => _solutionService.GetSolutions(questions));
        }

        [Test]
        public async Task GetSolutions_NullOptions_ReturnsOnlySolutions()
        {
            // Arrange
            var questions = new List<Question>
            {
                new Question { QuestionId = 1, QuestionType = "MCQ" }
            };

            var solutions = new List<Solution>
            {
                new Solution { QuestionId = 1, CorrectOptionAnswer = "A" }
            };

            _mockSolutionRepository.Setup(r => r.GetSolutions(It.IsAny<List<int>>()))
                                   .ReturnsAsync(solutions);

            _mockOptionsRepository.Setup(r => r.GetAllByQuestionIdAsync(It.IsAny<List<int>>()))
                                  .ReturnsAsync((List<Option>)null); 

            // Act
            var (resultSolutions, resultOptions) = await _solutionService.GetSolutions(questions);

            // Assert
            Assert.IsNotNull(resultSolutions);
            Assert.IsNull(resultOptions); 
            Assert.AreEqual(1, resultSolutions.Count);
        }

        [Test]
        public void GetSolutions_ExceptionInRepositories_ThrowsErrorInConnectingRepository()
        {
            // Arrange
            var questions = new List<Question>
            {
                new Question { QuestionId = 1, QuestionType = "MCQ" }
            };

            _mockSolutionRepository.Setup(r => r.GetSolutions(It.IsAny<List<int>>()))
                                   .ThrowsAsync(new Exception("Simulated exception"));

            // Act & Assert
            Assert.ThrowsAsync<ErrorInConnectingRepository>(async () => await _solutionService.GetSolutions(questions));
        }
    }
}
