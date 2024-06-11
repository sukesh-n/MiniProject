using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces.Test;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="admin")]
    //[Authorize(Roles = "organizer")]
    //[Authorize(Roles = "candidate")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("TestNotification")]
        public async Task<IActionResult> GetNotificationsForTest(string email,string role)
        {
            try
            {
                var checkNotification = await _notificationService.CheckNotification(email, role);
                return Ok(checkNotification);
            }
            catch
            {
                throw new UnableToFetchException("Error while getting assignment");
            }
        }
    }
}
