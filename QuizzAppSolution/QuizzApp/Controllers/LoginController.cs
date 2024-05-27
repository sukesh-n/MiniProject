﻿using Microsoft.AspNetCore.Mvc;
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
                throw new LoginErrorException("Unable to login as Admin");
            }
            
        }

        [HttpGet("OrganizerLogin")]
        public async Task<IActionResult> OrganizerLogin(LoginDTO loginDTO)
        {
            try
            {
                var organizerLogin = await _loginInterface.AdminLogin(loginDTO.Email, loginDTO.Password);
                organizerLogin.Role = "organizer";
                return Ok(organizerLogin);
            }
            catch
            {
                throw new LoginErrorException("Unable to login as Organizer");
            }

        }

        [HttpGet("CandidateLogin")]
        public async Task<IActionResult> CandidateLogin(LoginDTO loginDTO)
        {
            try
            {
                var candidateLogin = await _loginInterface.CandidateLogin(loginDTO.Email, loginDTO.Password);
                candidateLogin.Role = "candidate";
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
    }
}