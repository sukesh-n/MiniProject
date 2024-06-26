using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces.Test;
using System.Security.Claims;

namespace QuizzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    [Authorize(Roles = "admin, organizer, candidate")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("TestNotification")]
        public async Task<IActionResult> GetNotificationsForTest(string email, string role)
        {
            try
            {
                foreach (var claim in HttpContext.User.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }

                // Get the current user's claims from the token
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                Console.WriteLine(claimsIdentity);
                var EmailIdentity = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var RoleIdentity = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
                var NameIdentity = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                var userId = claimsIdentity.FindFirst("userid")?.Value;
                if (string.IsNullOrEmpty(EmailIdentity) || string.IsNullOrEmpty(RoleIdentity) || string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Invalid User");
                }
                if(EmailIdentity!=email || RoleIdentity!=role)
                {
                    return BadRequest("Invalid User");
                }

                // Use the extracted information as needed
                var checkNotification = await _notificationService.CheckNotification(EmailIdentity, RoleIdentity, userId);
                return Ok(checkNotification);
            }
            catch
            {
                throw new UnableToFetchException("Error while getting assignment");
            }
        }
    }
}
