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
        private readonly IAdminService _adminInterface;
        private readonly IQuestionService _questionService;
        

        public AdminController(IAdminService adminInterface, IQuestionService questionService)
        {
            _adminInterface = adminInterface;
            _questionService = questionService;
        }

        [HttpPut("AddQuestionWithSolution")]
        public async Task<IActionResult> AddQuestionsWithSolution(QuestionSolutionDTO questionSolutionDTO)
        {
            try
            {
                var result = await _questionService.AddQuestionWithAnswerAsync(questionSolutionDTO);
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
                var UpdateResult = await _questionService.UpdateQuestionsWithSolution(questionSolutionDTO);
                return Ok(UpdateResult);
            }
            catch
            {
                throw new UnableToUpdateException("Unable to update question");
            }
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetQuestionBasedOnCategory()
        {
            try
            {
                var GetQuestion = await _questionService.GetAllCategoriesAsync();
                return Ok(GetQuestion);
            }
            catch
            {
                throw new UnableToFetchException("Response to fetch cannot be implemented");
            }
        }
    }
}
