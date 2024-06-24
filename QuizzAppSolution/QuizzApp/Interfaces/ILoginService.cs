using QuizzApp.Models;
using QuizzApp.Models.DTO.UserServicesDTO;

namespace QuizzApp.Interfaces
{
    public interface ILoginService
    {
        public Task<LoginReturnDTO> CandidateLogin(string email, string password);
        public Task<LoginReturnDTO> OrganizerLogin(string email, string password);
        public Task<LoginReturnDTO> AdminLogin(string email, string password);
        public Task<User> RegisterUser(UserDTO userDTO);
        public Task<bool> DeleteUser(string? email, int? userId, string? role);
        public Task<User> UpdateUser(UserDTO userDTO);
        
    }
}
