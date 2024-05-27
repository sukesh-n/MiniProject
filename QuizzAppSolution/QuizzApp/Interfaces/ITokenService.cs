using QuizzApp.Models;

namespace QuizzApp.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
