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
    [Authorize(Roles = "candidate")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
           
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

        [HttpPut("GetCustomQuizz/{UserId}")]
        public async Task<IActionResult> GetRandomQuizz(QuestionSelectionDTO questionSelectionDTO,int UserId)
        {
            try
            {
                var GetRandomQuizz = await _candidateService.GetRandomQuizz(questionSelectionDTO,UserId);
               
                var questionDto = GetRandomQuizz.Item1;
                var testId = GetRandomQuizz.Item2;
                var assignmentNumber = GetRandomQuizz.Item3;

                return Ok(new { questionDto, testId, assignmentNumber });
               
                
            }
            catch
            {
                throw new UnableToFetchException("Unable to fetch");
            }
        }

        [HttpPut("TakeCustomQuizz")]
        public async Task<IActionResult> TakeCustomTest(List<QuestionDTO> questionDTO,int TestId, int AssignmentNumber, string email)
        {
            try
            {
                var TakeCustomTest = await _candidateService.TakeCustomTest(questionDTO,TestId,AssignmentNumber,email);
                
                return Ok(TakeCustomTest.Item2);
            }
            catch
            {
                throw new UnableToFetchException("Unable to fetch");
            }
        }



    }
}
