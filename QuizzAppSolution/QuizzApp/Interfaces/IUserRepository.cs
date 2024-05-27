using QuizzApp.Models;

namespace QuizzApp.Interfaces
{
    public interface IUserRepository : IRepository<int,User>
    {
        public Task<User> GetUserByEmailAsync(string email);
    }
}
