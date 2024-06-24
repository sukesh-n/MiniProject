using NUnit.Framework;
using Moq;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Services;
using QuizzApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizzApp.Interfaces.Solutions;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Models.DTO;

namespace QuizzApp.Tests.Services
{
    [TestFixture]
    public class QuestionServicesTest
    {
        private QuestionServices _questionService;
        private Mock<IQuestionRepository> _questionRepositoryMock;
        private Mock<ISolutionRepository> _solutionRepositoryMock;
        private Mock<IOptionsRepository> _optionRepositoryMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _solutionRepositoryMock = new Mock<ISolutionRepository>();
            _optionRepositoryMock = new Mock<IOptionsRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();

            _questionService = new QuestionServices(
                _questionRepositoryMock.Object,
                _solutionRepositoryMock.Object,
                _optionRepositoryMock.Object,
                _categoryRepositoryMock.Object);
        }

        [Test]
        public async Task AddQuestionWithAnswerAsync_ValidInput_Success()
        {
            // Arrange
            var questionSolutionDTO = new QuestionSolutionDTO
            {
                MainCategory = "TestCategory",
                SubCategory = "SubCategory",
                QuestionDescription = "Sample question",
                QuestionType = "MCQ",
                QuestionDifficultyLevel = 1,
                Options = new List<string> { "Option A", "Option B", "Option C" },
                CorrectOptionAnswer = "1"
            };

            _categoryRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Category>()))
                .ReturnsAsync(new Category { CategoryId = 1 });

            _questionRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Question>()))
                .ReturnsAsync(new Question { QuestionId = 1 });

            _solutionRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Solution>()))
                .ReturnsAsync(new Solution { SolutionId = 1 });

            _optionRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Option>()))
                .ReturnsAsync(new Option { Optionid = 1 });

            // Act
            var result = await _questionService.AddQuestionWithAnswerAsync(questionSolutionDTO);

            // Assert
            Assert.IsTrue(result);
            _questionRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Question>()), Times.Once);
            _solutionRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Solution>()), Times.Once);
            _optionRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Option>()), Times.Exactly(questionSolutionDTO.Options.Count));
        }

        [Test]
        public async Task GetAllCategoriesAsync_EmptyRepository_ExceptionThrown()
        {
            // Arrange
            _categoryRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Category>());

            // Act + Assert
            var ex = Assert.ThrowsAsync<EmptyDatabaseException>(async () => await _questionService.GetAllCategoriesAsync());
            StringAssert.Contains("No categories", ex.Message);
        }

        [Test]
        public async Task GetQuestionWithCategory_InvalidFormatExceptionThrown()
        {
            // Arrange
            var questionSelectionDTO = new QuestionSelectionDTO
            {
                SubCategory = "SubCategory"
                
            };

            // Act + Assert
            var ex = Assert.ThrowsAsync<UnableToFetchException>(async () => await _questionService.GetQuestionWithCategory(questionSelectionDTO));
            StringAssert.Contains("Unable to get Questions", ex.Message);
        }

        
    }
}
