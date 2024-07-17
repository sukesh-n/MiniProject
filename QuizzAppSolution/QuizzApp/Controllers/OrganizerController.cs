using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,organizer")]
    public class OrganizerController : ControllerBase
    {
        private readonly IOrganizerService _organizerService;

        public OrganizerController(IOrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        [HttpPut("AssignTest")]
        public async Task<IActionResult> AssignTestForUsers([FromBody]  AssignTestRequestDTO requestDTO)
        {
            try
            {
                var assign = await _organizerService.AssignTest(requestDTO.TestAssignDTO, requestDTO.QuestionSelectionDTO);
                return Ok(assign);
            }
            catch
            {
                throw new UnableToAddException("Unable to assign Question");
            }
        }
        [HttpPut("GetQuestions")]
        public async Task<IActionResult> GetQuestions(QuestionSelectionDTO questionSelectionDTO)
        {
            try
            {
                var getQuestion = await _organizerService.GenerateQuizzApiWithSolution(questionSelectionDTO);
                return Ok(getQuestion);
            }
            catch
            {
                throw new UnableToFetchException("Unable to fetch");
            }
        }

        [HttpGet("GetAllTestsByOrganizer/{userId}")]
        public async Task<IActionResult> GetAllTestsByOrganizer(int userId)
        {
            try
            {
                var getTests = await _organizerService.GetAllTestsByOrganizer(userId);
                return Ok(getTests);
            }
            catch
            {
                throw new UnableToFetchException("Unable to fetch");
            }
        }
        [HttpGet("GetTestDetails/{currentUserId}/{assignmentNumber}")]
        public async Task<IActionResult> GetTestDetails(int currentUserId, int assignmentNumber)
        {
            try
            {
                var getTestDetails = await _organizerService.GetTestDetails(assignmentNumber,currentUserId);
                return Ok(getTestDetails);
            }
            catch
            {
                throw new UnableToFetchException("Unable to fetch");
            }
        }
    }
}
