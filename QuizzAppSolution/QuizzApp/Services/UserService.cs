using QuizzApp.Interfaces;
using QuizzApp.Models;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Repositories;

namespace QuizzApp.Services
{
    public class UserService : ILoginInterface
    {
        public Task<User> AdminLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> CandidateLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> OrganizerLogin(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
