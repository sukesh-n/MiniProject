using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Models.DTO.UserServicesDTO;
using System.Data;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginInterface;

        public LoginController(ILoginService loginInterface)
        {
            _loginInterface = loginInterface;
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin(LoginDTO loginDTO)
        {
            try
            {
                var adminLogin = await _loginInterface.AdminLogin(loginDTO.Email, loginDTO.Password);
                //adminLogin.Role = "admin";
                return Ok(adminLogin);
            }
            catch
            {
                throw new LoginErrorException("Unable to login as Admin");
            }
            
        }

        [HttpPost("OrganizerLogin")]
        public async Task<IActionResult> OrganizerLogin(LoginDTO loginDTO)
        {
            try
            {
                var organizerLogin = await _loginInterface.AdminLogin(loginDTO.Email, loginDTO.Password);
                //organizerLogin.Role = "organizer";
                return Ok(organizerLogin);
            }
            catch
            {
                throw new LoginErrorException("Unable to login as Organizer");
            }

        }

        [HttpPost("CandidateLogin")]
        public async Task<IActionResult> CandidateLogin(LoginDTO loginDTO)
        {
            try
            {
                var candidateLogin = await _loginInterface.CandidateLogin(loginDTO.Email, loginDTO.Password);
                //candidateLogin.Role = "candidate";
                return Ok(candidateLogin);
            }
            catch
            {
                throw new LoginErrorException("Unable to login as candidate");
            }

        }

        [HttpPut("RegisterUser")]

        public async Task<IActionResult> RegisterUser(UserDTO userDTO)
        {
            try
            {
                var registerUser = await _loginInterface.RegisterUser(userDTO);
                return Ok(registerUser);
            }
            catch
            {
                throw new UnableToAddUserException("Error in adding User");
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<bool> DeleteUser(string? email,int? userId,string? role)
        {
            try
            {
                var DeleteResult = await _loginInterface.DeleteUser(email, userId, role);
                return DeleteResult;
            }
            catch (Exception ex) 
            {
                throw;
            }
            catch
            {
                    throw new UnableToDeleteException();
            }
        }
    }
}
