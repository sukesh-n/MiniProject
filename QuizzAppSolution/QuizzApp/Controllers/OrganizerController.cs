using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "organizer")]
    public class OrganizerController
    {

    }
}
