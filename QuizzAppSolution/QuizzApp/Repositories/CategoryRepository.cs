using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using Microsoft.EntityFrameworkCore;
namespace QuizzApp.Repositories
{
    public class CategoryRepository : IRepository<int, Category>
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

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
