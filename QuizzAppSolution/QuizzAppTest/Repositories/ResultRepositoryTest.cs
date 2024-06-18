using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuizzApp.Context;
using QuizzApp.Exceptions;
using QuizzApp.Models;
using QuizzApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzAppTest.Repositories
{
    [TestFixture]
    public class ResultRepositoryTest
    {
        private QuizzAppContext _context;
        private ResultRepository _resultRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<QuizzAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new QuizzAppContext(options);
            _context.Database.EnsureDeleted(); 
            _context.Database.EnsureCreated(); 

            
            SeedDatabase();

            _resultRepository = new ResultRepository(_context);
        }

        private void SeedDatabase()
        {
            var results = new List<Result>
            {
                new Result { ResultId = 1, TestId = 1, score = 80 },
                new Result { ResultId = 2, TestId = 2, score = 75 },
                new Result { ResultId = 3, TestId = 1, score = 90 }
            };

            _context.results.AddRange(results);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        
        [Test]
        public async Task AddAsync_ShouldAddResult()
        {
            // Arrange
            var newResult = new Result { TestId = 3, score = 85 };

            // Act
            var addedResult = await _resultRepository.AddAsync(newResult);

            // Assert
            Assert.IsNotNull(addedResult);
            Assert.AreNotEqual(0, addedResult.ResultId); 
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllResults()
        {
            // Act
            var results = await _resultRepository.GetAllAsync();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, ((List<Result>)results).Count); 
        }

        
        [Test]
        public async Task GetAsync_ExistingResultId_ShouldReturnResult()
        {
            // Arrange
            int existingResultId = 1;

            // Act
            var result = await _resultRepository.GetAsync(existingResultId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(existingResultId, result.ResultId);
        }

        // Test for GetAsync method with non-existing ResultId
        [Test]
        public void GetAsync_NonExistingResultId_ShouldThrowException()
        {
            // Arrange
            int nonExistingResultId = 100;

            // Act & Assert
            Assert.ThrowsAsync<ErrorInConnectingRepository>(async () => await _resultRepository.GetAsync(nonExistingResultId));
        }
    }
}
