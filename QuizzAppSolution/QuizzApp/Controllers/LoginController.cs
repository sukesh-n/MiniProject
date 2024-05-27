using Microsoft.AspNetCore.Mvc;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Models.DTO;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginInterface _loginInterface;

        public LoginController(ILoginInterface loginInterface)
        {
            _loginInterface = loginInterface;
        }

        [HttpGet("AdminLogin")]
        public async Task<IActionResult> AdminLogin(LoginDTO loginDTO)
        {
            try
            {
                var adminLogin = await _loginInterface.AdminLogin(loginDTO.Email, loginDTO.Password);
                adminLogin.Role = "admin";
                return Ok(adminLogin);
            }
            catch
            {
                throw new LoginErrorException("Unable to login");
            }
            
        }
    }
}
