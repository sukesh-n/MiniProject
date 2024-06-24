using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuizzApp.Context;
using QuizzApp.Exceptions;
using QuizzApp.Models;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzApp.Tests.Repositories
{
    [TestFixture]
    public class AssignedTestEmailRepositoryTests
    {
        private QuizzAppContext _context;
        private AssignedTestEmailRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<QuizzAppContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            _context = new QuizzAppContext(options);
            _repository = new AssignedTestEmailRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AddEmailsForTest_WithValidData_ReturnsTrue()
        {
            // Arrange
            var testData = new List<AssignedTestEmailDTO>
    {
        new AssignedTestEmailDTO { Email = "test1@example.com", AssignmentNumber = 1 },
        new AssignedTestEmailDTO { Email = "test2@example.com", AssignmentNumber = 2 }
    };

            // Act
            var result = await _repository.AddEmailsForTest(1, testData);

            // Assert
            Assert.IsTrue(result);

            var addedEntities = _context.assignedTestEmails.ToList();
            Assert.AreEqual(testData.Count, addedEntities.Count);
            Assert.IsTrue(addedEntities.Any(e => e.Email == "test1@example.com"));
            Assert.IsTrue(addedEntities.Any(e => e.Email == "test2@example.com"));
        }

        [Test]
        public async Task AddEmailsForTest_WithDuplicateData_ReturnsFalse()
        {
            // Arrange
            var initialData = new List<AssignedTestEmail>
    {
        new AssignedTestEmail { Email = "test1@example.com", AssignmentNumber = 1 }
    };
            await _context.assignedTestEmails.AddRangeAsync(initialData);
            await _context.SaveChangesAsync();

            var testData = new List<AssignedTestEmailDTO>
    {
        new AssignedTestEmailDTO { Email = "test1@example.com", AssignmentNumber = 1 }
    };

            // Act
            var result = await _repository.AddEmailsForTest(1, testData);

            // Assert
            Assert.IsFalse(result);
            var existingEntities = _context.assignedTestEmails.ToList();
            Assert.AreEqual(initialData.Count, existingEntities.Count);
        }

    }
}
