using Microsoft.AspNetCore.Mvc;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.Test;
using QuizzApp.Exceptions;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TestController : ControllerBase
    {
        private readonly ITestService _testInterface;

        public TestController(ITestService testInterface)
        {
            _testInterface = testInterface;
        }
        [HttpGet("SelectCustomQuestions")]
        public async Task<IActionResult> SelectQuestions(QuestionSelectionDTO questionSelectionDTO)
        {
            try
            {
                var QuestionSelection = await _testInterface.ChooseQuestion(questionSelectionDTO);
                return Ok(QuestionSelection);
            }
            catch
            {
                throw new UnableToFetchException("Unable to get Questions");
            }
        }


        [HttpPost("GetTestQuestions")]
        public async Task<IActionResult> GetTestQuestions([FromBody] GetTestQuestionsDTO request)
        {
            try
            {
                var TestQuestions = await _testInterface.GetTestQuestions(request.AssignmentNumber, request.Email);
                return Ok(new { TestQuestions = TestQuestions.Item1, QuestionOptions = TestQuestions.Item2 });

            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AttendTest")] // No need for parameters in the route
        public async Task<IActionResult> AttendTest([FromBody] AttendTestRequest request)
        {
            try
            {
                var TestAssign = await _testInterface.AttendTest(
                    request.QuestionDTO,
                    request.AssignmentNumber,
                    request.Email
                );
                var QuestionDTO = TestAssign.Item1;
                var ScoreDTO = TestAssign.Item2;
                var Solution = TestAssign.Item3;
                return Ok(new { QuestionDTO, ScoreDTO,Solution });
            }
            catch
            {
                throw new UnableToFetchException("Unable to process test submission");
            }
        }
        public class AttendTestRequest
        {
            public List<QuestionDTO> QuestionDTO { get; set; }
            public int AssignmentNumber { get; set; }
            public string Email { get; set; }
        }



    }
}
