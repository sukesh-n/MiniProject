﻿using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Exceptions;

namespace QuizzApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly QuizzAppContext _context;

        public CategoryRepository(QuizzAppContext context)
        {
            _context = context;
        }

        public async Task<Category> AddAsync(Category entity)
        {
            var existingMainCategory = await _context.categories.FirstOrDefaultAsync(c => c.MainCategory == entity.MainCategory);

            if (existingMainCategory != null)
            {
                
                var existingCategory = await _context.categories.FirstOrDefaultAsync(c => c.MainCategory == entity.MainCategory && c.SubCategory == entity.SubCategory);

                if (existingCategory != null)
                {                    
                    return existingCategory;
                }
                else
                {                 
                    var result = await _context.categories.AddAsync(entity);
                    await _context.SaveChangesAsync();                    
                    return result.Entity;
                }
            }
            else
            {
                var result = await _context.categories.AddAsync(entity);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public Task<Category> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                var categories = await _context.categories.ToListAsync();
                return categories;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository("Error occurred while fetching all categories.", ex);
            }
        }


        public Task<Category> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<int>> GetCategoryId(string mainCategory, string subCategory)
        {
            try
            {
                IQueryable<Category> query = _context.categories.Where(c => c.MainCategory == mainCategory);
                if (!string.IsNullOrEmpty(subCategory))
                {
                    query = query.Where(c => c.SubCategory == subCategory);
                }
                else
                {
                    query = query.Where(c => c.SubCategory == null);
                }
                var categoryIds = await query.Select(c => c.CategoryId).ToListAsync();
                return categoryIds;
            }
            catch (NullReferenceException ex)
            {
                return new List<int>();
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository("Error while retrieving category IDs.", ex);
            }
        }

        public Task<Category> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
