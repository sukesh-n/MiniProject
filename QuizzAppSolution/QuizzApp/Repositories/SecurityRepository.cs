using Microsoft.EntityFrameworkCore;
using QuizzApp.Context;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class SecurityRepository : IRepository<int, Security>
    {
        private readonly QuizzAppContext _context;

        public SecurityRepository(QuizzAppContext context)
        {
            _context = context;
        }

        public async Task<Security> AddAsync(Security entity)
        {            
            try
            {
                var result = await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository("Unable to add security", ex);
            }
        }

        public async Task<Security> DeleteAsync(int Key)
        {
            try
            {
                var securityToDelete = await _context.security.FindAsync(Key);

                if (securityToDelete != null)
                {
                    _context.security.Remove(securityToDelete);
                    await _context.SaveChangesAsync();
                }

                return securityToDelete;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository(ex.Message, ex);
            }
        }

        public Task<IEnumerable<Security>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Security> GetAsync(int Key)
        {
            try
            {
                var result = await _context.security.FirstOrDefaultAsync(x => x.UserId == Key);
                await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository("Unable to add security", ex);
            }
        }

        public Task<Security> UpdateAsync(Security entity)
        {
            try
            {
                var result = _context.Update(entity);
                _context.SaveChanges();
                if(result == null)
                {
                    throw new ErrorInConnectingRepository("Unable to update security");
                }
                return Task.FromResult(result.Entity);
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository("Unable to update security", ex);
            }
        }
    }
}
