using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuizzApp.Context;
using QuizzApp.Models;
using QuizzApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static QuizzApp.Models.Option;

namespace QuizzAppTest.Repositories
{
    public class OptionsRepositoryTest
    {
        private QuizzAppContext _context;
        private OptionsRepository _repository;

        [SetUp]
        public void Setup()
        {
            
            var options = new DbContextOptionsBuilder<QuizzAppContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new QuizzAppContext(options);
            _repository = new OptionsRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddAsync_ShouldAddOption_WhenOptionIsNew()
        {
            // Arrange
            var newOption = new Option { QuestionId = 1, Value = "Option 1", OptionDescription= (OptionDescriptionType)1, Optionid=1 };

            // Act
            var result = await _repository.AddAsync(newOption);


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.options.Count());
            Assert.AreEqual("Option 1", result.Value);
        }

        [Test]
        public async Task AddAsync_ShouldReturnNull_WhenOptionIsDuplicate()
        {
            // Arrange
            var option = new Option { QuestionId = 1, Value = "Option 1", OptionDescription = (OptionDescriptionType)1, Optionid = 1 };
            _context.options.Add(option);
            await _context.SaveChangesAsync();

            var duplicateOption = new Option { QuestionId = 1, Value = "Option 1", OptionDescription = (OptionDescriptionType)1, Optionid = 1 };

            // Act
            var result = await _repository.AddAsync(duplicateOption);

            // Assert
            Assert.IsNull(result);
            Assert.AreEqual(1, _context.options.Count());
        }

        [Test]
        public async Task GetAllByQuestionIdAsync_ShouldReturnOptions_ForGivenQuestionIds()
        {
            // Arrange
            var options = new List<Option>
            {
                new Option { QuestionId = 1, Value = "Option 1", OptionDescription = (OptionDescriptionType)1,Optionid=1 },
                new Option { QuestionId = 2, Value = "Option 2", OptionDescription = (OptionDescriptionType) 1,Optionid = 1 },
                new Option { QuestionId = 1, Value = "Option 3", OptionDescription = (OptionDescriptionType) 1,Optionid = 2 }
            };
            _context.options.AddRange(options);
            await _context.SaveChangesAsync();

            var questionIds = new List<int> { 1 };

            // Act
            var result = await _repository.GetAllByQuestionIdAsync(questionIds);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }
}
