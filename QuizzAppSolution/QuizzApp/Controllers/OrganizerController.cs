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
    //[Authorize(Roles ="admin")]
    //[Authorize(Roles = "organizer")]
    public class OrganizerController : ControllerBase
    {
        private readonly IOrganizerService _organizerService;

        public OrganizerController(IOrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        [HttpPut("AssignTest")]
        public async Task<IActionResult> AssignTestForUsers(AssignTestRequestDTO requestDTO)
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
    }
}
