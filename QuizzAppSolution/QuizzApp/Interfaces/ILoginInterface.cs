using QuizzApp.Models;

namespace QuizzApp.Interfaces
{
    public interface ILoginInterface
    {
        public Task<User> CandidateLogin(string email, string password);
        public Task<User> OrganizerLogin(string email, string password);
        public Task<User> AdminLogin(string email, string password);
    }
}
