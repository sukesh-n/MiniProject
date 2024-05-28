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
        private readonly ITestInterface _testInterface;

        public TestController(ITestInterface testInterface)
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
    }
}
