using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Exceptions;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Models.DTO;
namespace QuizzApp.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuizzAppContext _context;

        public QuestionRepository(QuizzAppContext context)
        {
            _context = context;
        }

        public async Task<Question> AddAsync(Question entity)
        {
            try
            {
                var DuplicateCheck = _context.questions.FirstOrDefault(q => q.QuestionDescription == entity.QuestionDescription);
                if(DuplicateCheck == null)
                {
                    var result = await _context.questions.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return result.Entity;
                }
                return DuplicateCheck;
            }
            catch
            {
                throw new ErrorInConnectingRepository("Unable to add Question");
            }
        }

        public Task<Question> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Question>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Question> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Question>> GetFilteredQuestions(QuestionSelectionDTO questionSelectionDTO, List<int> categoryIds)
        {
            try
            {
                IQueryable<Question> query = _context.questions;

                
                if (questionSelectionDTO != null)
                {
                    if (questionSelectionDTO.DifficultyLevel != 0)
                    {
                        query = query.Where(q => q.DifficultyLevel == questionSelectionDTO.DifficultyLevel);
                    }
                    if (!string.IsNullOrEmpty(questionSelectionDTO.Type))
                    {
                        query = query.Where(q => q.QuestionType == questionSelectionDTO.Type);
                    }
                }

                if (categoryIds != null && categoryIds.Any())
                {
                    query = query.Where(q => categoryIds.Contains(q.CategoryId));
                }

                var filteredQuestions = await query.ToListAsync();

                
                var random = new Random();
                var randomizedQuestions = filteredQuestions.OrderBy(q => random.Next()).ToList();

                
                if (questionSelectionDTO?.NoOfQuestions > 0)
                {
                    randomizedQuestions = randomizedQuestions.Take(questionSelectionDTO.NoOfQuestions).ToList();
                }

                return randomizedQuestions;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository("Unable to retrieve random filtered questions", ex);
            }
        }

        public Task<List<QuestionDTO>> GetQuestionById(List<int> QuestionIds)
        {
            try
            {
                var questions = _context.questions
                    .Where(q => QuestionIds.Contains(q.QuestionId))
                    .Select(q => new QuestionDTO
                    {
                        QuestionId = q.QuestionId,
                        QuestionDescription = q.QuestionDescription,
                        QuestionType = q.QuestionType,
                        DifficultyLevel = q.DifficultyLevel,
                        CategoryId = q.CategoryId
                    })
                    .ToListAsync();
                return questions;
            }
            catch (Exception ex)
            {
                throw new UnableToFetchException("Unable to retrieve questions by Id", ex);
            }
        }

        public async Task<List<QuestionTypeCount>> GetTypesAndTheirCount()
        {
            try
            {
                var typeCounts = await _context.questions
                    .GroupBy(q => q.QuestionType)
                    .Select(g => new QuestionTypeCount
                    {
                        Type = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync();

                return typeCounts;
            }
            catch (Exception ex)
            {
                throw new UnableToFetchException("Unable to retrieve question types and their counts", ex);
            }
        }

        public Task<List<QuestionTypeCount>> GetTypesAndTheirCount(int DifficultyLevel)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuestionTypeCountBasedOnCategory>> GetTypesAndTheirCountByCategory(string MainCategory, string SubCategory)
        {
            throw new NotImplementedException();
        }

        public Task<Question> UpdateAsync(Question entity)
        {
            throw new NotImplementedException();
        }
    }
}
