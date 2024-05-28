using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Models.DTO;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminInterface _adminInterface;

        public AdminController(IAdminInterface adminInterface)
        {
            _adminInterface = adminInterface;
        }

        [HttpPut("AddQuestionWithSolution")]
        public async Task<IActionResult> AddQuestionsWithSolution(QuestionSolutionDTO questionSolutionDTO)
        {
            try
            {
                var result = await _adminInterface.AddQuestionWithAnswerAsync(questionSolutionDTO);
                return Ok(result);
            }
            catch
            {
                throw new UnableToAddException("Questions cannot be added");
            }
        }

        [HttpPost("UpdateQuestionAndItsSolution")]
        public async Task<IActionResult> UpdateQuestion(QuestionSolutionDTO questionSolutionDTO)
        {
            try
            {
                var UpdateResult = await _adminInterface.UpdateQuestionsWithSolution(questionSolutionDTO);
                return Ok(UpdateResult);
            }
            catch
            {
                throw new UnableToUpdateException("Unable to update question");
            }
        }

        [HttpGet("GetQuestionByCategory")]
        public async Task<IActionResult> GetQuestionBasedOnCategory(QuestionWithCategoryDTO questionWithCategoryDTO)
        {
            try
            {
                var GetQuestion = await _adminInterface.GetQuestionWithCategory(questionWithCategoryDTO);
                return Ok(GetQuestion);
            }
            catch
            {
                throw new UnableToFetchException("Response to fetch cannot be implemented");
            }
        }
    }
}
