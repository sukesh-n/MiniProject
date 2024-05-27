using Microsoft.EntityFrameworkCore;
using QuizzApp.Context;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Models;

namespace QuizzApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QuizzAppContext _context;

        public UserRepository(QuizzAppContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User entity)
        {
            try
            {
                var VerifyDuplicate = await _context.users.FirstOrDefaultAsync(u=>u.UserEmail==entity.UserEmail);
                if (VerifyDuplicate != null)
                {
                    if (entity.Role.ToLower() == "admin")
                    {
                        throw new UnauthorizedException("You are not allowed this role");
                    }
                    if (VerifyDuplicate.Role==entity.Role)
                    {
                        throw new UserAlreadyExistException($"User {entity.UserEmail} already with role {entity.Role}");
                    }
                    
                }
                var result = await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch
            {
                throw new ErrorInConnectingRepository("User cannot be added to Database");
            }
        }

        public async Task<User> DeleteAsync(int Key)
        {
            try
            {
                var GetUserById = await GetAsync(Key);
                if (GetUserById != null)
                {
                    var result = _context.Remove(GetUserById);
                    await _context.SaveChangesAsync();
                    
                }
                return GetUserById;
            }
            catch
            {
                throw new ErrorInConnectingRepository("Unable to delete user");
            }
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(int Key)
        {
            try
            {
                var GetById = await _context.users.FirstOrDefaultAsync(u => u.UserId == Key);
                if(GetById == null)
                {
                    throw new EmptyDatabaseException("No Entity in Users");
                    
                }
                return GetById;
            }
            catch
            {
                throw new EmptyDatabaseException("No Entity in Users");
            }
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                var result = await _context.users.FirstOrDefaultAsync(u => u.UserEmail == email);
                if(result == null)
                {
                    throw new EmptyDatabaseException("No email");
                }
                return result;
            }
            catch
            {
                throw new ErrorInConnectingRepository("Unable to retrieve user by email");
            }
        }
        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
