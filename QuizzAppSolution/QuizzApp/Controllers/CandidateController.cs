using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "candidate")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CandidateController(ICandidateService candidateService, IHttpContextAccessor httpContextAccessor)
        {
            _candidateService = candidateService;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpGet("LoginForTest")]
        public async Task<IActionResult> AssignedTestLogin(int TestId, string RoomCode, int userId)
        {
            try
            {
                //var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                //if (userId == null)
                //{
                //    throw new LoginErrorException("Unable To connect test server");
                //}
                var testLoginCandidate = await _candidateService.GetMyQuestion(TestId, userId);
                return Ok(testLoginCandidate);
            }
            catch
            {
                throw new LoginErrorException("Unable to get your test");
            }
        }

        [HttpPut("AttendAssignedTest")]
        //[Authorize($"candidate")]
        public async Task<IActionResult> AttendAssignedTest(QuestionDTO questionDTO)
        {
            try
            {
                var AttendTest = await _candidateService.AttendAssignedTest(questionDTO);
                return Ok(AttendTest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
