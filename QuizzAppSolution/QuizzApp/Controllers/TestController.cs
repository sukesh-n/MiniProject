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


        [HttpGet("GetTestQuestions")]
        public async Task<IActionResult> GetTestQuestions(int AssignmentNumber,string email)
        {
            try
            {
                var TestQuestions = await _testInterface.GetTestQuestions(AssignmentNumber,email);
                return Ok(TestQuestions);
            }
            catch
            {
                throw new UnableToFetchException("Unable to get Questions");
            }
        }

        [HttpPut("AttendTest")]
        public async Task<IActionResult> AttendTest(List<QuestionDTO> questionDTO,int AssignmentNumber, string email)
        {
            try
            {
                var TestAssign = await _testInterface.AttendTest(questionDTO,AssignmentNumber,email);
                var QuestionDTO = TestAssign.Item1;
                var ScoreDTO = TestAssign.Item2;
                return Ok(new {QuestionDTO,ScoreDTO});
            }
            catch
            {
                throw new UnableToFetchException("Unable to get Questions");
            }
        }
    }
}
