using QuizzApp.Models;

namespace QuizzApp.Interfaces
{
    public interface IUserRepository : IRepository<int,User>
    {
        public Task<List<User>> GetUserByEmailAsync(string email);
        public Task<User> DeleteUserAsync(string? email, int? userId, string? role);
        public Task<List<User>> GetAllDetailsByEmailsAsync(List<string> emails);
        public Task<List<User>> GetAllDetailsByUserEmailAsync(string email);

    }
}
