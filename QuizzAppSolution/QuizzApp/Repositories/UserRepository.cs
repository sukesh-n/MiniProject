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
                var existingUser = await _context.users.FirstOrDefaultAsync(u => u.UserEmail == entity.UserEmail && u.Role == entity.Role);

                if (existingUser != null)
                {
                    if (entity.Role.ToLower() == "admin")
                    {
                        throw new UnauthorizedException("You are not allowed this role");
                    }
                    throw new UserAlreadyExistException($"User {entity.UserEmail} already exists with role {entity.Role}");
                }

                var result = await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (UnauthorizedException)
            {
                throw;
            }
            catch (UserAlreadyExistException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository("User cannot be added to Database", ex);
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

        public async Task<User> DeleteUserAsync(string? email, int? userId, string? role)
        {
            try
            {
                User userToDelete = null;

                if (email != null && role != null)
                {
                    userToDelete = await _context.users.FirstOrDefaultAsync(u => u.UserEmail == email && u.Role == role);
                }
                else if (userId != null)
                {
                    userToDelete = await _context.users.FirstOrDefaultAsync(u => u.UserId == userId);
                }
                if (userToDelete != null)
                {
                    _context.users.Remove(userToDelete);
                    await _context.SaveChangesAsync();
                }

                return userToDelete;
            }
            catch(Exception ex)
            {
                throw new ErrorInConnectingRepository();
            }

        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllDetailsByEmailsAsync(List<string> emails)
        {
            try
            {
                List<User> UserData = new List<User>();

                foreach (var email in emails)
                {
                    var users = await _context.users
                        .Where(u => u.UserEmail == email)
                        .ToListAsync();

                    UserData.AddRange(users);
                }
                return UserData;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository();
            }
        }

        public Task<List<User>> GetAllDetailsByUserEmailAsync(string email)
        {
            try
            {
                List<User> users=new List<User>();
                users = _context.users
                    .Where(u => u.UserEmail == email)
                    .ToList();
                return Task.FromResult(users);
            }
            catch (Exception ex) {
                throw new ErrorInConnectingRepository();
            }
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
