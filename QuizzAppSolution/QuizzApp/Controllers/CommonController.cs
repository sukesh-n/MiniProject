using Microsoft.AspNetCore.Mvc;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Live;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        IQuestionService _questionService;
        IUpdateDb updateDb;

        public CommonController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _questionService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("UpdateDb")]
        public async Task<IActionResult> UpdateDb()
        {
            try
            {
                var result = await updateDb.UpdateAssignedTestEmail();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
