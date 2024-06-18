using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizzApp.Context;
using QuizzApp.Models;
using QuizzApp.Repositories;
using QuizzApp.Exceptions;

namespace QuizzApp.Tests.Repositories
{
    [TestFixture]
    public class CategoryRepositoryTest
    {
        private QuizzAppContext _context;
        private CategoryRepository _categoryRepository;

        [SetUp]
        public void Setup()
        {
            
            var options = new DbContextOptionsBuilder<QuizzAppContext>()
                .UseInMemoryDatabase(databaseName: "InMemory_Category_Database")
                .Options;

            _context = new QuizzAppContext(options);
            _categoryRepository = new CategoryRepository(_context);

            
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            
            if (_context.categories.Any())
            {
                return; 
            }

            var categories = new List<Category>
            {
                new Category { CategoryId = 1, MainCategory = "Science", SubCategory = "Physics" },
                new Category { CategoryId = 2, MainCategory = "Science", SubCategory = "Chemistry" },
                new Category { CategoryId = 3, MainCategory = "History", SubCategory = "World War II" },
                new Category { CategoryId = 4, MainCategory = "History", SubCategory = "World War I" },
                new Category { CategoryId = 5, MainCategory = "Mathematics", SubCategory = "Algebra" }
            };

            _context.AddRange(categories);
            _context.SaveChanges();
        }


        [Test]
        public async Task AddAsync_CategoryDoesNotExist_ShouldAddNewCategory()
        {
            // Arrange
            var newCategory = new Category { MainCategory = "Science", SubCategory = "Biology" };

            // Act
            var result = await _categoryRepository.AddAsync(newCategory);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newCategory.MainCategory, result.MainCategory);
            Assert.AreEqual(newCategory.SubCategory, result.SubCategory);

            
            var addedCategory = await _context.categories.FindAsync(result.CategoryId);
            Assert.IsNotNull(addedCategory);
            Assert.AreEqual(newCategory.MainCategory, addedCategory.MainCategory);
            Assert.AreEqual(newCategory.SubCategory, addedCategory.SubCategory);
        }

        [Test]
        public async Task GetCategoryId_ValidInput_ShouldReturnCategoryIds()
        {
            // Arrange
            var mainCategory = "Mathematics";
            var subCategory = "Algebra";

            // Act
            var result = await _categoryRepository.GetCategoryId(mainCategory, subCategory);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.Contains(5, result);
        }

        [Test]
        public async Task AddAsync_CategoryAlreadyExists_ShouldReturnExistingCategory()
        {
            // Arrange
            var existingCategory = new Category { MainCategory = "Science", SubCategory = "Physics" };

            // Act
            var result = await _categoryRepository.AddAsync(existingCategory);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(existingCategory.MainCategory, result.MainCategory);
            Assert.AreEqual(existingCategory.SubCategory, result.SubCategory);
            Assert.AreEqual(5, await _context.categories.CountAsync());
        }

        [Test]
        public async Task DeleteAsync_CategoryExists_ShouldDeleteCategory()
        {
            // Arrange
            var categoryIdToDelete = 1;

            // Act
            await _categoryRepository.DeleteAsync(categoryIdToDelete);

            // Assert
            var deletedCategory = await _context.categories.FindAsync(categoryIdToDelete);
            Assert.IsNull(deletedCategory);
        }

        [Test]
        public void DeleteAsync_CategoryNotFound_ShouldThrowException()
        {
            // Arrange
            var nonExistingCategoryId = 99; // Assuming this ID does not exist in the database

            // Act & Assert
            var ex = Assert.ThrowsAsync<ErrorInConnectingRepository>(async () => await _categoryRepository.DeleteAsync(nonExistingCategoryId));
            Assert.AreEqual("Error while deleting category.", ex.Message);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllCategories()
        {
            // Act
            var categories = await _categoryRepository.GetAllAsync();

            // Assert
            Assert.IsNotNull(categories);
            Assert.AreEqual(5, categories.Count()); 
        }

        [Test]
        public async Task GetAsync_CategoryExists_ShouldReturnCategory()
        {
            // Arrange
            var categoryId = 5;

            // Act
            var category = await _categoryRepository.GetAsync(categoryId);

            // Assert
            Assert.IsNotNull(category);
            Assert.AreEqual(categoryId, category.CategoryId);
        }

        [Test]
        public void GetAsync_CategoryNotFound_ShouldThrowException()
        {
            // Arrange
            var nonExistingCategoryId = 99; // Assuming this ID does not exist in the database

            // Act & Assert
            var ex = Assert.ThrowsAsync<ErrorInConnectingRepository>(async () => await _categoryRepository.GetAsync(nonExistingCategoryId));
            Assert.AreEqual("Error while retrieving category.", ex.Message);
        }


        [Test]
        public async Task UpdateAsync_CategoryExists_ShouldUpdateCategory()
        {
            // Arrange
            var categoryIdToUpdate = 2;
            var updatedCategory = new Category { CategoryId = categoryIdToUpdate, MainCategory = "Science", SubCategory = "Astronom" };

            // Act
            var result = await _categoryRepository.UpdateAsync(updatedCategory);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedCategory.SubCategory, result.SubCategory);

            
            var updatedCategoryFromDb = await _context.categories.FindAsync(categoryIdToUpdate);
            Assert.IsNotNull(updatedCategoryFromDb);
            Assert.AreEqual(updatedCategory.SubCategory, updatedCategoryFromDb.SubCategory);
        }

        [Test]
        public void UpdateAsync_CategoryNotFound_ShouldThrowException()
        {
            // Arrange
            var nonExistingCategory = new Category { CategoryId = 99, MainCategory = "NonExisting", SubCategory = "Category" };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ErrorInConnectingRepository>(async () => await _categoryRepository.UpdateAsync(nonExistingCategory));
            Assert.AreEqual("Unable to update category.", ex.Message);
        }
    }
}
