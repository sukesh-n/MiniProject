using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "candidate")]
    public class CandidateController
    {

    }
}
