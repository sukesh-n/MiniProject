﻿using QuizzApp.Models;
using QuizzApp.Models.DTO.UserServicesDTO;

namespace QuizzApp.Interfaces
{
    public interface ILoginInterface
    {
        public Task<LoginReturnDTO> CandidateLogin(string email, string password);
        public Task<LoginReturnDTO> OrganizerLogin(string email, string password);
        public Task<LoginReturnDTO> AdminLogin(string email, string password);
        public Task<User> RegisterUser(UserDTO userDTO);
    }
}
